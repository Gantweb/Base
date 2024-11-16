Imports System.IO
Imports System.IO.Compression
Imports System.Security.Cryptography
Imports System.Text
Imports WpfApp1.AppTools.Wrapper

Namespace AppTools
    Friend Module AppFile

        Friend Function ScriviFile(filePath As String, content As String, Optional compress As Boolean = False, Optional encrypt As Boolean = False) As Boolean
            Return ExecuteWithWrapper(Function()
                                          Dim data As Byte() = Encoding.UTF8.GetBytes(content)

                                          If compress Then
                                              data = CompressData(data)
                                          End If

                                          If encrypt Then
                                              data = EncryptData(data)
                                          End If

                                          File.WriteAllBytes(filePath, data)
                                          Return True
                                      End Function)
        End Function

        Friend Function LeggiFile(filePath As String, Optional decompress As Boolean = False, Optional decrypt As Boolean = False) As String
            Return ExecuteWithWrapper(Function()
                                          Dim data As Byte() = File.ReadAllBytes(filePath)

                                          If decrypt Then
                                              data = DecryptData(data)
                                          End If

                                          If decompress Then
                                              data = DecompressData(data)
                                          End If

                                          Return Encoding.UTF8.GetString(data)
                                      End Function)
        End Function

        Private Function ExecuteWithWrapper(Of T)(operation As Func(Of T)) As T
            Dim obfuscator As New ObfuscationLayer(Of T)(operation)
            Return obfuscator.Execute()
        End Function

        Private Function CompressData(data As Byte()) As Byte()
            Using outputStream As New MemoryStream()
                Using compressionStream As New GZipStream(outputStream, CompressionMode.Compress)
                    compressionStream.Write(data, 0, data.Length)
                End Using
                Return outputStream.ToArray()
            End Using
        End Function

        Private Function DecompressData(data As Byte()) As Byte()
            Using inputStream As New MemoryStream(data)
                Using decompressionStream As New GZipStream(inputStream, CompressionMode.Decompress)
                    Using reader As New MemoryStream()
                        decompressionStream.CopyTo(reader)
                        Return reader.ToArray()
                    End Using
                End Using
            End Using
        End Function

        Private Function EncryptData(data As Byte()) As Byte()
            Using aes As Aes = aes.Create()
                aes.Key = Encoding.UTF8.GetBytes("ChaveSecreta1234")
                aes.IV = Encoding.UTF8.GetBytes("IVSimples1234567")
                Using encryptor As ICryptoTransform = aes.CreateEncryptor(aes.Key, aes.IV)
                    Using ms As New MemoryStream()
                        Using cs As New CryptoStream(ms, encryptor, CryptoStreamMode.Write)
                            cs.Write(data, 0, data.Length)
                        End Using
                        Return ms.ToArray()
                    End Using
                End Using
            End Using
        End Function

        Private Function DecryptData(data As Byte()) As Byte()
            Using aes As Aes = aes.Create()
                aes.Key = Encoding.UTF8.GetBytes("ChaveSecreta1234")
                aes.IV = Encoding.UTF8.GetBytes("IVSimples1234567")
                Using decryptor As ICryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV)
                    Using ms As New MemoryStream(data)
                        Using cs As New CryptoStream(ms, decryptor, CryptoStreamMode.Read)
                            Using reader As New MemoryStream()
                                cs.CopyTo(reader)
                                Return reader.ToArray()
                            End Using
                        End Using
                    End Using
                End Using
            End Using
        End Function
    End Module

    Friend Class ObfuscationLayer(Of T)

        Private operation As Func(Of T)

        Public Sub New(op As Func(Of T))
            Me.operation = op
        End Sub

        Public Function Execute() As T
            Return Me.operation.Invoke()
        End Function

    End Class

End Namespace