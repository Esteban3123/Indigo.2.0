
'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Julian Cardozo
' Created          : 03-03-2011
'
' Last Modified By : Jose Paez
' Last Modified On : 23-03-2011
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base
''' <summary>
''' Interfaz ICRUDBase que contiene metodos y propiedades para ser heredada por otras interfaces
''' </summary>
Public Interface ICrudBase

#Region "Metodos"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Sub Buscar()
    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Sub Guardar()
    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Sub Nuevo()
    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Sub Deshacer()
    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Sub Eliminar()
    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Sub OpenSearch()
    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    WriteOnly Property Mensaje(ByVal Icono As EeventViewerImages) As String
    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -> Muestra Guardar | False -> Muestra Actualizar
    ''' </summary>
    ''' <remarks></remarks>
    Sub LogicaBotonActualizar(existeDatos As Boolean)


#End Region

End Interface
