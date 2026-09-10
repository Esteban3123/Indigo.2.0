'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Rafael E. Patiño
' Created          : 2013-09-10
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.Data.Xpo

#End Region


''' <summary>
''' Modelo del frontal de parametros Interfaces de glosas
''' </summary>
Public Class MInterfacesParameters
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String
#End Region

#Region "Builders"



    Sub New(Tag As String)
        _tagForm = Tag
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Funcion para Cargar los tipos de documento para notas contables
    ''' </summary>
    ''' <param name="containerName">nombre del conteneder a traer datos</param>
    ''' <returns>Cargar los tipos de documento para notas contables</returns>
    Public Async Function ListTypeDocument(ByVal containerName As String) As Task(Of List(Of SP_TypeDocumentList_Result))
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListTypeDocumentAsync(containerName, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' consulta parametros por codigo
    ''' </summary>
    ''' <param name="ContainerName">codigo de contenedor</param>
    ''' <returns>objeto tipo parametro</returns>
    Public Async Function GetInterfacesParameters(ByVal ContainerName As String) As Task(Of GlosasParametersInterface)
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetInterfacesParametersAsync(ContainerName, Me._indigoSessionValues)
    End Function


    ''' <summary>
    ''' Crea o actualiza el conjunto de parametros de interfaz
    ''' </summary>
    ''' <param name="InterfaceParameter">Conjunto de parametros para configuracion de la interfaces</param>
    ''' <returns>Resultado de la accion</returns>
    Public Async Function SaveInterfaceParameter(InterfaceParameter As GlosasParametersInterface) As Task(Of ActionResult)
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.SaveInterfaceParameterAsync(InterfaceParameter, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para Cargar las Cuentas Contables DGH
    ''' </summary>
    ''' <param name="container">contenedor de interfaz</param>
    ''' <returns>Lista de cuentas contables</returns>
    ''' <remarks></remarks>
    Public Async Function ListAccounts(ByVal container As String) As Task(Of List(Of SP_AccountsList_Result))
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListAccountsAsync(container, Me._indigoSessionValues)
    End Function


    ''' <summary>
    ''' Funcion para cargar los conceptos contables
    ''' </summary>
    ''' <param name="container">contenedor de interfaz</param>
    ''' <param name="TypeConcept">tipo concepto de cartera</param>
    ''' <returns>lista de conceptos de cartera</returns>
    ''' <remarks></remarks>
    Public Async Function ListAccountingConcept(ByVal container As String, ByVal TypeConcept As String) As Task(Of List(Of SP_AccountingConceptList_Result))
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListAccountingConceptAsync(container, TypeConcept, Me._indigoSessionValues)
    End Function

    ' ''' <summary>
    ' ''' Funcion para cargar las configuraciones realizadas por metodo privado
    ' ''' </summary>
    ' ''' <param name="CompanyId">Codigo de la compañia</param>
    ' ''' <param name="Years">año de configuracion</param>
    ' ''' <returns>Objeto tipo parametro privado</returns>
    'Public Async Function GetPrivateParametersInterface(ByVal CompanyId As String, ByVal Years As String) As Task(Of GlosasPrivateParametersInterface)
    '    Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetPrivateParametersInterfaceAsync(CompanyId, Years, Me._indigoSessionValues)
    'End Function

    ' ''' <summary>
    ' ''' Funcion para Guardar parametros metodo privado
    ' ''' </summary>
    ' ''' <param name="PrivateParameterInterface">Objecto de parametros metodo privado</param>
    ' ''' <returns>Si la Accion se realizo correctamente</returns>
    'Public Async Function savePrivateParametersInterface(ByVal PrivateParameterInterface As GlosasPrivateParametersInterface) As Task(Of ActionResult)
    '    Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.savePrivateParametersInterfaceAsync(PrivateParameterInterface, Me._indigoSessionValues)
    'End Function


    ' ''' <summary>
    ' ''' Funcion para borrar parametro metodo privado
    ' ''' </summary>
    ' ''' <param name="PrivateParameterInterface">Objecto de parametros metodo privado</param>
    ' ''' <returns>Si la Accion se realizo correctamente</returns>
    'Public Async Function deletePrivateParametersInterface(ByVal PrivateParameterInterface As GlosasPrivateParametersInterface) As Task(Of Boolean)
    '    Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.deletePrivateParametersInterfaceAsync(PrivateParameterInterface, Me._indigoSessionValues)
    'End Function


    ''' <summary>
    ''' Funcion para cargar las configuraciones realizadas por metodo publico
    ''' </summary>
    ''' <param name="CompanyId">Codigo de la compañia</param>
    ''' <param name="Years">año de configuracion</param>
    ''' <returns>Objeto tipo parametro publico</returns>
    'Public Async Function GetPublicParametersInterface(ByVal CompanyId As String, ByVal Years As String) As Task(Of GlosasPublicParametersInterface)
    '    Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetPublicParametersInterfaceAsync(CompanyId, Years, Me._indigoSessionValues)
    'End Function

    ' ''' <summary>
    ' ''' Funcion para Guardar parametros metodo public
    ' ''' </summary>
    ' ''' <param name="PublicParameterInterface">Objecto de parametros metodo public</param>
    ' ''' <returns>Si la Accion se realizo correctamente</returns>
    'Public Async Function savePublicParametersInterface(ByVal PublicParameterInterface As GlosasPublicParametersInterface) As Task(Of ActionResult)
    '    Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.savePublicParametersInterfaceAsync(PublicParameterInterface, Me._indigoSessionValues)
    'End Function

    ' ''' <summary>
    ' ''' Funcion para borrar parametro metodo publico
    ' ''' </summary>
    ' ''' <param name="PublicParameterInterface">Objecto de parametros metodo publico</param>
    ' ''' <returns>Si la Accion se realizo correctamente</returns>
    'Public Async Function deletePublicParametersInterface(ByVal PublicParameterInterface As GlosasPublicParametersInterface) As Task(Of Boolean)
    '    Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.deletePublicParametersInterfaceAsync(PublicParameterInterface, Me._indigoSessionValues)
    'End Function

    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As Domain.Entities.BlockRecord) As Task(Of ActionResult(Of Domain.Entities.BlockRecord))
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBlockRecordAsync(Record, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para eliminar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecord(ByVal Record As Domain.Entities.BlockRecord) As Task(Of ActionResult)
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteBlockRecordAsync(Record, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of Domain.Entities.BlockRecord)
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetBlockRecordByIdformAndIdRecordAsync(IdForm, IdRecord, Me._indigoSessionValues)
    End Function


    ''' <summary>
    ''' Listar los campos nulls de la base de datos y porder customizar 
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetFieldsNULL() As Task(Of DataSet)
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("GlosasParametersInterface", Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Validar contenedor
    ''' </summary>
    ''' <param name="Container">Nombre de contenedor</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ValidateContainers(ByVal Container As String) As Task(Of Boolean)
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ValidateContainerAsync(Container, Me._indigoSessionValues)
    End Function

    ''' <summary>
    ''' Lista configuracion cuenta FOX metodo privado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function List_AccountSettingsFOX_PrivateMethod() As Task(Of List(Of AccountSettingsFOX_PrivateMethod))
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.List_AccountSettingsFOX_PrivateMethodAsync(Me._indigoSessionValues)
    End Function
    ''' <summary>
    ''' Lista configuracion cuenta FOX metodo publico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function List_AccountSettingsFOX_PublicMethod() As Task(Of List(Of AccountSettingsFOX_PublicMethod))
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.List_AccountSettingsFOX_PublicMethodAsync(Me._indigoSessionValues)
    End Function
    ''' <summary>
    ''' Lista configuracion cuenta NET metodo privaado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function List_AccountSettingsNET_PublicMethod() As Task(Of List(Of AccountSettingsNET_PublicMethod))
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.List_AccountSettingsNET_PublicMethodAsync(Me._indigoSessionValues)
    End Function
    ''' <summary>
    ''' Lista configuracion cuenta NET metodo privaado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function List_AccountSettingsNET_PrivateMethod() As Task(Of List(Of AccountSettingsNET_PrivateMethod))
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.List_AccountSettingsNET_PrivateMethodAsync(Me._indigoSessionValues)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
