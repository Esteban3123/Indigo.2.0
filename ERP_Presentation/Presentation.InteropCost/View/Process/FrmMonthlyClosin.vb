'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 19-06-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.InteropCost.MVP

#End Region

Public Class FrmMonthlyClosin
    Implements IMonthlyClosing

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(value As String)

        End Set
    End Property

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

    Public WriteOnly Property ActionsOnControls As Boolean Implements IMonthlyClosing.ActionsOnControls
        Set(value As Boolean)

        End Set
    End Property

    Public Sub AssigningValues() Implements IMonthlyClosing.AssigningValues

    End Sub

    Public Sub CleanControls() Implements IMonthlyClosing.CleanControls

    End Sub

    Public Sub LoadControls() Implements IMonthlyClosing.LoadControls

    End Sub

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IMonthlyClosing.MyLayoutControl
        Get

        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IMonthlyClosing.MyTag
        Get

        End Get
    End Property

    Private Sub FrmMontClose_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
