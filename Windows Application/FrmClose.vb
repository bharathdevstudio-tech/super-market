Imports System.IO
Imports System.Data.SqlClient

Public Class FrmClose
    Private Sub FrmClose_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
      

    End Sub

    Private Sub btnYes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYes.Click

        FrmMDICommon.Close()

    End Sub

    Private Sub btnNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNo.Click
        Me.Close()
    End Sub
End Class

