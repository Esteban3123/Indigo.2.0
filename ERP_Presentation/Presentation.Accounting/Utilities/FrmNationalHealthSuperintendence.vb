Public Class FrmNationalHealthSuperintendence
    Implements Presentation.Base.ICrudBase

    ''' <summary>
    ''' Item buscar del control de usuarios
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Item Deshacer del control de usuarios
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer

    End Sub

    ''' <summary>
    ''' Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Item Guardar del control de usuarios
    ''' </summary>
    Public Sub Guardar() Implements Base.IcrudBase.Guardar

    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -> Muestra Guardar | False -> Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
        Set(value As String)

        End Set
    End Property

    ''' <summary>
    ''' Item Nuevo del control de usuario
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' Abre el frontal de busqueda
    ''' Abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch

    End Sub
End Class