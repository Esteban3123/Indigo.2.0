'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Jorge Leonardo Vernaza
' Created          : 15-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias importadas"
#End Region

''' <summary>
''' Clase con toda la funcionalidad del control para agregar nuevos Registros a una rejilla
''' </summary>
Public Class CtrExportGlosa

#Region "variables"
    Public Event ClickExport(sender As Object, e As EventArgs)
#End Region

#Region "Eventos"

    Private Sub INDbtnAddRecord_Click(sender As Object, e As EventArgs) Handles INDbtnAddRecord.Click
        RaiseEvent ClickExport(sender, e)
    End Sub

#End Region

End Class
