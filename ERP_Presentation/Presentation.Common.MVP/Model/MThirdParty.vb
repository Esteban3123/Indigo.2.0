'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 03-07-2013
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
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.CrossCutting.Xpo.Base
Imports DevExpress.Data.Filtering

#End Region

''' <summary>
''' Modelo de conexion con los servicios distribuidos de los terceros
''' </summary>
Public Class MThirdParty
    Inherits ModelBase
    Implements IDisposable

#Region "Builder"

    Private _indigoSessionValues As SessionValues

    Private _tagForm As String

    Public Shared TAG As String = "532"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="TAG">Tag del formulario</param>
    ''' <remarks></remarks>
    Public Sub New(TAG As String)
        MyBase.New(TAG)
        _tagForm = TAG
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' lista los documentos del tercero
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListThirdPartyDocuments(thirdPartyId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListThirdPartyDocuments(thirdPartyId)
    End Function

    ''' <summary>
    ''' Funcion para obtener el tercero Asincrono
    ''' </summary>
    ''' <param name="Nit">Codigo del tercero</param>
    ''' <returns></returns>
    Public Async Function GetThirdPartyAsync(ByVal Nit As String) As Task(Of ThirdParty)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Dim OriginalTransactionalContainer = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer
        Dim TransactionalContainer As String = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer
        If Not String.IsNullOrEmpty(Infrastructure.CrossCutting.Base.SessionValues.Instance.FoundationalContainer) Then
            If Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf IsNot Nothing Then
                If Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional IsNot Nothing Then
                    If Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission IsNot Nothing Then
                        Dim vieForm = Infrastructure.CrossCutting.Base.SessionValues.Instance.ListFormPermission.FirstOrDefault(Function(f) f.Id = Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional)
                        If vieForm IsNot Nothing AndAlso vieForm.IsFoundational Then
                            TransactionalContainer = Infrastructure.CrossCutting.Base.SessionValues.Instance.FoundationalContainer
                        End If
                    End If
                    Infrastructure.CrossCutting.Base.SessionValues.Instance.AuditMessageWcf.Functional = Nothing
                End If
            End If
        End If
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = TransactionalContainer
        Dim result = Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetThirdPartyByNitAsync(Nit, _indigoSessionValues)
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = OriginalTransactionalContainer
        Return result
    End Function

    ''' <summary>
    ''' Funcion para obtener el tercero 
    ''' </summary>
    ''' <param name="Nit">Codigo del tercero</param>
    ''' <returns></returns>
    Public Function GetThirdParty(ByVal Nit As String) As ThirdParty
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetThirdPartyByNit(Nit, _indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para obtener la persona modo asincrono
    ''' </summary>
    ''' <param name="code">Codigo de la persona</param>
    ''' <returns></returns>
    Public Async Function GetPersonAsync(ByVal code As String) As Task(Of Person)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetPersonByIdentificationAsync(code, _indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para obtener la persona  
    ''' </summary>
    ''' <param name="code">Codigo de la persona</param>
    ''' <returns></returns>
    Public Function GetPerson(ByVal code As String) As Person
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetPersonByIdentification(code, _indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para guardar el tercero modo asincrono
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <returns></returns>
    Public Async Function SaveThirdPartyAsync(ByVal Record As ThirdParty) As Task(Of Boolean)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Dim OriginalTransactionalContainer = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer
        Dim TransactionalContainer As String = Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = TransactionalContainer
        Dim result = Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveThirdPartyAsync(Record, _indigoSessionValues)
        Infrastructure.CrossCutting.Base.SessionValues.Instance.TransactionalContainer = OriginalTransactionalContainer
        Return result
    End Function

    ''' <summary>
    ''' Funcion para guardar el tercero
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <returns></returns>
    Public Function SaveThirdParty(ByVal Record As ThirdParty) As Boolean
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveThirdParty(Record, _indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para borrar el tercero
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <returns></returns>
    Public Function DeleteThirdParty(ByVal Record As ThirdParty) As Boolean
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteThirdParty(Record, _indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para borrar el tercero
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <returns></returns>
    Public Async Function DeleteThirdPartyAsync(ByVal Record As ThirdParty) As Task(Of Boolean)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteThirdPartyAsync(Record, _indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para listar todos los terceros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListAllThirdPartyAsync() As Task(Of List(Of ThirdParty))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllThirdPartyAsync(_indigoSessionValues)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetFieldsNULL() As Object
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("ThirdParty", _indigoSessionValues)
    End Function

    ''' <summary>
    ''' Obtiene una dependencia por el codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetThirdPartyById(ByVal id As Integer) As Task(Of ThirdParty)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetThirdPartyByIdAsync(id, _indigoSessionValues)
    End Function

    Public Function GetThirdPartyByIdSimple(ByVal id As Integer) As ThirdParty
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetThirdPartyById(id, _indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion para listar todos los terceros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListAllCityAsync() As Task(Of List(Of City))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllCityAsync(_indigoSessionValues)
    End Function

    Public Async Function UpdateStateThirdPartyAsync(id As Integer, status As Boolean) As Task(Of Boolean)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.UpdateStateThirdPartyAsync(id, status, _indigoSessionValues)
    End Function

    ''' <summary>
    ''' Funcion que nos retorna si la longitud del Nit es correcta
    ''' </summary>
    Public Async Function ValidateLenghtNit(ThirdPartyNit As String, IdentificationAcronyms As String) As Task(Of Domain.Base.Entities.ActionResult(Of Boolean))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ValidateLenghtNitAsync(ThirdPartyNit, IdentificationAcronyms, _indigoSessionValues)
    End Function

    ''' <summary>
    ''' obtiene el tipo de identificacion por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Async Function GetADTIPOIDENTIFICAXpoById(Id As Integer) As Task(Of ADTIPOIDENTIFICAXpo)

        Return Await Task.Factory.StartNew(Function() As ADTIPOIDENTIFICAXpo
                                               Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CrystalService.GetXPOObject(Of ADTIPOIDENTIFICAXpo)($"ID = {Id}")
                                           End Function)

    End Function

    ''' <summary>
    ''' obtiene el obj de tipo de identificación por su sigla
    ''' </summary>
    ''' <param name="Abbreviation"></param>
    ''' <returns></returns>
    Public Async Function GetADTIPOIDENTIFICAXpoByAbbreviation(Abbreviation As String) As Task(Of ADTIPOIDENTIFICAXpo)

        Return Await Task.Factory.StartNew(Function() As ADTIPOIDENTIFICAXpo
                                               Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CrystalService.GetXPOObject(Of ADTIPOIDENTIFICAXpo)($"SIGLA = '{Abbreviation}'")
                                           End Function)

    End Function

    Public Function GetThirdParty() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AccountingRepository.CommonThirdPartyXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & True)
        Dim _classEntity = session.GetClassInfo(GetType(AccountingRepository.CommonThirdPartyXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Nit;ContributionTypeName;RetentionTypeName;Name;PersonId.IdentificationTypeName;PersonId.IdentificationNumber;PersonId.FirstName;PersonId.FirstLastName;NitName;PersonId.IdentificationType;PersonId.IdentificacionCityId.Descripcion;RetentionType;ContributionType;State;StateName", criteria)
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
