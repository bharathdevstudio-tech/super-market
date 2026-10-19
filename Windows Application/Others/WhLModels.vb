Imports System.Runtime.Serialization

Namespace Models

    <Serializable()> _
    Public Class ServerStatus
        Public Property connected As Boolean
        Public Property sending As Boolean
        Public Property qr As String
        Public Property lastError As String
        Public Property phone As String
    End Class

    Public Class ContactResult
        Public Property Number As String
        Public Property Success As Boolean
        Public Property Message As String
    End Class

    Public Class SendResponse
        Public Property success As Boolean
        Public Property message As String
        Public Property results As List(Of ContactResultItem)
    End Class

    <Serializable()> _
    Public Class ContactResultItem
        Public Property number As String
        Public Property status As String
        Public Property [error] As String
    End Class

End Namespace
