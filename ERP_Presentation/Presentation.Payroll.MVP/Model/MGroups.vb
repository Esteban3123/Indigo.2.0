'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 19-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports Presentation.Base
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo

#End Region
''' <summary>
''' Realiza la conexion con los servicios del grupo
''' </summary>
Public Class MGroups
    Inherits ModelBase
    Implements IDisposable

    Public Shared TAG As String = "521"

    Sub New(tag As String)
        MyBase.New(tag)
    End Sub


#Region "Properties"
    Dim Indigo As SessionValues = SessionValues.Instance
#End Region

#Region "Methods"

    ''' <summary>
    ''' Funcion para Cargar los tipos de documento para notas contables
    ''' </summary>
    ''' <param name="containerName">nombre del conteneder a traer datos</param>
    ''' <returns>Cargar los tipos de documento para notas contables</returns>
    Public Async Function ListTypeDocument(ByVal containerName As String) As Task(Of List(Of Domain.Entities.SP_TypeDocumentList_Result))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListTypeDocumentAsync(containerName, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el grupo de acuerdo al codigo 
    ''' </summary>
    ''' <param name="code">Codigo del grupo</param>
    ''' <returns>el grupo</returns>
    Public Function GetGroup(ByVal code As String) As Group
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetGroup(code, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el grupo de acuerdo al codigo de manera asincrona
    ''' </summary>
    ''' <param name="code">Codigo del grupo</param>
    ''' <returns>El grupo</returns>
    Public Async Function GetGroupAsync(ByVal code As String) As Task(Of Group)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetGroupAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el grupo de acuerdo al id de manera asincrona
    ''' </summary>
    ''' <param name="id">id del grupo</param>
    ''' <returns>El grupo</returns>
    Public Async Function GetGroupByIdAsync(ByVal id As String) As Task(Of Group)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetGroupByIdAsync(id, Indigo)
    End Function

    ''' <summary>
    '''Obtiene los grupos filtrados por empresa
    ''' </summary>
    ''' <param name="companyid">id empresa</param>
    ''' <returns>El grupo</returns>
    Public Async Function GetGroupsByCompanyId(ByVal companyid As String) As Task(Of List(Of Group))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetGroupsByCompanyIdAsync(companyid, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el listado de grupos 
    ''' </summary>
    ''' <returns>Listado de grupos</returns>
    Public Function ListAllGroups() As List(Of Group)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllGroups(Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el listado de grupos 
    ''' </summary>
    ''' <returns>Listado de grupos</returns>
    Public Function ListGroupsByStatus(Status As Boolean) As List(Of Group)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListGroupsByStatus(Status, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el listado de grupos 
    ''' </summary>
    ''' <returns>Listado de grupos</returns>
    Public Function ListGroupsByStatusAsync(Status As Boolean) As Task(Of List(Of Group))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListGroupsByStatusAsync(Status, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el listado de grupos asincrono
    ''' </summary>
    ''' <returns>Listado de grupos</returns>
    Public Async Function ListAllGroupsAsync() As Task(Of List(Of Group))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllGroupsAsync(Indigo)
    End Function

    ''' <summary>
    ''' Guarda los cambios del grupo
    ''' </summary>
    ''' <param name="group">Grupo</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Function SaveGroup(ByVal group As Group) As Boolean
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveGroup(group, Indigo)
    End Function

    ''' <summary>
    ''' Guarda los cambios del grupo asincrono
    ''' </summary>
    ''' <param name="group">Grupo</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function SaveGroupAsync(ByVal group As Group) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveGroupAsync(group, Indigo)
    End Function

    ''' <summary>
    ''' Borra el grupo
    ''' </summary>
    ''' <param name="group">Grupo</param>
    ''' <returns>Si se realizo o no el borrado</returns>
    Public Function DeleteGroup(ByVal group As Group) As ActionMessageResult(Of Group)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteGroup(group, Indigo)
    End Function

    ''' <summary>
    ''' Borra el grupo asincrono
    ''' </summary>
    ''' <param name="group">Grupo</param>
    ''' <returns>Si se realizo o no el borrado</returns>
    Public Async Function DeleteGroupAsync(ByVal group As Group) As Task(Of ActionMessageResult(Of Group))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteGroupAsync(group, Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetFieldsNULL() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("Group", Indigo)
    End Function

    ''' <summary>
    '''Obtiene los grupos filtrados por empresa
    ''' </summary>
    ''' <param name="groupId">id Grupo</param>
    ''' <returns>El grupo</returns>
    Public Async Function GetGroupsLiquidationById(ByVal groupId As String) As Task(Of List(Of Group))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetGroupLiquidationByIdAsync(groupId, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Cargar las Cuentas Contables DGH
    ''' </summary>
    ''' <param name="container">contenedor de interfaz</param>
    ''' <returns>Lista de cuentas contables</returns>
    ''' <remarks></remarks>
    Public Async Function ListAccounts(ByVal container As String) As Task(Of List(Of Domain.Entities.SP_AccountsList_Result))
        Me.Indigo.AuditMessageWcf.Functional = TAG
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListAccountsAsync(container, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para Cargar las Cuentas Contables de Vie
    ''' </summary>
    ''' <param name="container">contenedor de interfaz</param>
    ''' <returns>Lista de cuentas contables</returns>
    ''' <remarks></remarks>
    Public Async Function ListAccountsVie() As Task(Of List(Of Domain.Entities.MainAccounts))
        Me.Indigo.AuditMessageWcf.Functional = TAG
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAllAcountsAsync()
    End Function

    Public Async Function GetAccont(IdAccount As Integer) As Task(Of Domain.Entities.MainAccounts)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAccountByIdAsync(IdAccount, False, Indigo)
    End Function

    'Public Async Function ListJournalVoucherTypeVie() As Task(Of List(Of Domain.Entities.JournalVoucherTypes))
    '    Me.Indigo.AuditMessageWcf.Functional = TAG
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.jou
    'End Function

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="State"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateGroup(Code As String, State As Boolean) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GroupChangeStateAsync(Code, State, Indigo)
    End Function

    ''' <summary>
    ''' Lista los terceros
    ''' </summary>
    Public Function ListAdjustmentConceptXpo() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).PayrollService.ListConceptsPayrollByStatus(True)
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
