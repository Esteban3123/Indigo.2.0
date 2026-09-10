Public Class FrmMultipleCreditor
    Implements Presentation.Base.IcrudBase


    Public Sub Buscar() Implements Base.IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements Base.IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
        Set(value As String)

        End Set
    End Property

    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch

    End Sub
End Class