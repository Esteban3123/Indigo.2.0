'***********************************************************************
' Assembly         : Infrastructure.Data.BillingRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-08-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class BillingControlRepository
    Inherits GenericRepository(Of BillingControl)
    Implements IBillingControlRepository

    'Contexto de Tesoreria
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un registro de control de tesoreria por consecutivo del documento
    ''' </summary>
    ''' <param name="DocumentNumber">The document number.</param>
    ''' <param name="DocumentType"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' DocumentNumber
    ''' or
    ''' DocumentType
    ''' </exception>
    Public Function GetBillingControlByDocumentNumber(DocumentNumber As String, Optional DocumentType As Integer = 0) As BillingControl Implements IBillingControlRepository.GetBillingControlByDocumentNumber
        If String.IsNullOrEmpty(DocumentNumber) Then
            Throw New ArgumentNullException("DocumentNumber")
        End If
        If DocumentType = 0 Then
            Throw New ArgumentNullException("DocumentType")
        End If
        Dim query = (From tc In _context.BillingControl Where tc.DocumentNumber.Equals(DocumentNumber) And tc.DocumentType = DocumentType Select tc).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From tc In _context.BillingControl.AsNoTracking() Where tc.DocumentNumber.Equals(DocumentNumber) And tc.DocumentType = DocumentType Select tc).FirstOrDefault()
            Return query
        Else
            Return New BillingControl()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un registro de control de los documentos de tesoreria por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetBillingControlById(Id As Integer) As BillingControl Implements IBillingControlRepository.GetBillingControlById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From tc In _context.BillingControl Where tc.Id = Id Select tc).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From tc In _context.BillingControl.AsNoTracking() Where tc.Id = Id Select tc).FirstOrDefault()
            Return query
        Else
            Return New BillingControl()
        End If
    End Function

    ''' <summary>
    ''' Obtiene los centros de atención
    ''' </summary>
    ''' <param name="UserCode"></param>
    ''' <param name="GroupCode"></param>
    ''' <param name="Container"></param>
    ''' <returns></returns>
    Public Function SP_ListCareCenterHis(UserCode As String, GroupCode As String, Container As String) As List(Of SP_ListCareCenterHis_Result) Implements IBillingControlRepository.SP_ListCareCenterHis
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ListCareCenterHis(UserCode, GroupCode, Container).ToList()
    End Function

    ''' <summary>
    ''' Obtiene las unidades funcionales
    ''' </summary>
    ''' <param name="CareCenterCode"></param>
    ''' <param name="UserCode"></param>
    ''' <param name="GroupCode"></param>
    ''' <param name="Container"></param>
    ''' <returns></returns>
    Public Function SP_ListFunctionalUnitHis(CareCenterCode As String, UserCode As String, GroupCode As String, Container As String) As List(Of SP_ListFunctionalUnitHis_Result) Implements IBillingControlRepository.SP_ListFunctionalUnitHis
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ListFunctionalUnitHis(CareCenterCode, UserCode, GroupCode, Container).ToList()
    End Function

    ''' <summary>
    ''' Valida el proceso de ingreso en control cuentas ambulatorios
    ''' </summary>
    ''' <param name="AdmissionNumber"></param>
    ''' <param name="IsCurrentAdmission"></param>
    ''' <param name="Xml"></param>
    ''' <returns></returns>
    Public Function SP_ProcessAccountControlAmbulatory(AdmissionNumber As String, IsCurrentAdmission As Boolean, Xml As String) As SP_ProcessAccountControlAmbulatory_Result Implements IBillingControlRepository.SP_ProcessAccountControlAmbulatory
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ProcessAccountControlAmbulatory(AdmissionNumber, IsCurrentAdmission, Xml).SingleOrDefault()
    End Function

End Class
