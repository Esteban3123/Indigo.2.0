'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Jorge Leonardo Vernaza
' Created          : 20-06-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Clase que contiene todo el comportamiento del control de actualizar en asincrono
''' </summary>
Public Class CtrUpdateLoad

    Public Event LoadDataSource()

    ''' <summary>
    ''' Metodo para hacer que el control cambie su diseño entre cargando y actualizar
    ''' </summary>
    ''' <param name="Updating">True si se quiere mostrar el icono actualizando y false si quiere mostrar el icono de actualizar</param>
    Public Sub StadeControl(ByVal Updating As Boolean)
        If Updating = False Then
            INDbtnUpdate.Visible = True
            INDpeAsyncLoad.Visible = False
        Else
            INDbtnUpdate.Visible = False
            INDpeAsyncLoad.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' Evento Click en el boton Actulizar que dispara el enveto LoadDataSource para actulizar 
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDbtnUpdate_Click(sender As Object, e As EventArgs) Handles INDbtnUpdate.Click
        RaiseEvent LoadDataSource()
    End Sub
End Class
