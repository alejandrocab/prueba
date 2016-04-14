Imports iTextSharp.text.pdf
Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim mstrRutaPdf As String = "C:\Users\usuario\Desktop\JavaEE.pdf"
        Dim contenido As String = Me.ParsePdfText(mstrRutaPdf)
        MsgBox(contenido)
    End Sub

    Public Function ParsePdfText(ByVal sourcePDF As String, _
                                  Optional ByVal fromPageNum As Integer = 0, _
                                  Optional ByVal toPageNum As Integer = 0) As String

        Dim sb As New System.Text.StringBuilder()
        Try
            Dim reader As New PdfReader(sourcePDF)
            Dim pageBytes() As Byte = Nothing
            Dim token As PRTokeniser = Nothing
            Dim tknType As Integer = -1
            Dim tknValue As String = String.Empty

            If fromPageNum = 0 Then
                fromPageNum = 1
            End If
            If toPageNum = 0 Then
                toPageNum = reader.NumberOfPages
            End If

            If fromPageNum > toPageNum Then
                Throw New ApplicationException("Parameter error: The value of fromPageNum can " & _
                                           "not be larger than the value of toPageNum")
            End If

            For i As Integer = fromPageNum To toPageNum Step 1
                pageBytes = reader.GetPageContent(i)
                If Not IsNothing(pageBytes) Then
                    '.Cast(Of iTextSharp.text.pdf.RandomAccessFileOrArray)

                    ' token = New PRTokeniser(pageBytes)
                    While token.NextToken()
                        tknType = token.TokenType()
                        tknValue = token.StringValue
                        If tknType = PRTokeniser.TokType.STRING Then
                            sb.Append(token.StringValue)
                            'I need to add these additional tests to properly add whitespace to the output string
                        ElseIf tknType = 1 AndAlso tknValue = "-600" Then
                            sb.Append(" ")
                        ElseIf tknType = 10 AndAlso tknValue = "TJ" Then
                            sb.Append(" ")
                        End If
                    End While
                End If
            Next i
        Catch ex As Exception
            MessageBox.Show("Exception occured. " & ex.Message)
            Return String.Empty
        End Try
        Return sb.ToString()
    End Function
End Class
