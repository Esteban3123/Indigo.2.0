'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 03-05-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Presentation.Base
Imports Domain.Base.Entities
#End Region
''' <summary>
''' Esta clase tiene el modelo del patron MVP implementado en los Tipos de telefono
''' </summary>
Public Class MPhoneType
    Inherits ModelBase
    Implements IDisposable

    Shared TAG As String = "527"

    Public Sub New()
        MyBase.New(TAG)
    End Sub
#Region "Methods"
    ''' <summary>
    ''' Obtener el listado de los Tipos de telefono
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllPhoneType() As List(Of PhoneType)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllPhoneType(Indigo)
    End Function
    ''' <summary>
    ''' Obtener un Tipo por su codigo
    ''' </summary>
    ''' <param name="code">El codigo del nivel de tipo .</param>
    ''' <returns>El Tipo de telefono</returns>
    Public Function GetPhoneType(ByVal code As String) As Object
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetPhoneType(code, Indigo)
    End Function
    ''' <summary>
    ''' Obtener un Tipo por su codigo modo asincrono
    ''' </summary>
    ''' <param name="code">El codigo del nivel de tipo .</param>
    ''' <returns>El Tipo de telefono</returns>
    Public Async Function GetPhoneTypeAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetPhoneTypeAsync(code, Indigo)
    End Function


    ''' <summary>
    ''' Graba el Tipo de telefono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el Tipo de Telefono</returns>
    Public Function SavePhoneType(ByVal reg As PhoneType) As Boolean
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SavePhoneType(reg, Indigo)
    End Function
    ''' <summary>
    ''' Graba el Tipo de telefono modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el Tipo de Telefono</returns>
    Public Async Function SavePhoneTypeAsync(ByVal reg As PhoneType) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SavePhoneTypeAsync(reg, Indigo)
    End Function
    ''' <summary>
    ''' Elimina el Tipo de telefono modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el Tipo de telefono</returns>
    Public Async Function DeletePhoneTypeAsync(ByVal reg As PhoneType) As Task(Of ActionMessageResult(Of PhoneType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeletePhoneTypeAsync(reg, Indigo)
    End Function
    ''' <summary>
    ''' Elimina el Tipo de telefono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el Tipo de telefono</returns>
    Public Function DeletePhoneType(ByVal reg As PhoneType) As ActionMessageResult(Of PhoneType)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeletePhoneType(reg, Indigo)
    End Function


    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULL("PhoneType", Me.Indigo)
    End Function
#End Region
#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
