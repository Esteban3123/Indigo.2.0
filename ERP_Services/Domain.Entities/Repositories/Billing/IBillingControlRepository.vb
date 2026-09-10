'************************************************************
' Assembly         : Domain.Contract
' Author           : Diego Andres Roldan
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IBillingControlRepository
    Inherits IRepository(Of BillingControl)

    ''' <summary>
    ''' Obtiene un registro de control de los documentos de tesoreria por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBillingControlById(Id As Integer) As BillingControl

    ''' <summary>
    ''' Obtiene un registro de control de tesoreria por consecutivo del documento
    ''' </summary>
    ''' <param name="DocumentNumber">The document number.</param>
    ''' <returns></returns>
    Function GetBillingControlByDocumentNumber(DocumentNumber As String, Optional DocumentType As Integer = 0) As BillingControl

    ''' <summary>
    ''' Obtiene los centros de atención
    ''' </summary>
    ''' <returns></returns>
    Function SP_ListCareCenterHis(UserCode As String, GroupCode As String, Container As String) As List(Of SP_ListCareCenterHis_Result)

    ''' <summary>
    ''' Obtiene las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    Function SP_ListFunctionalUnitHis(CareCenterCode As String, UserCode As String, GroupCode As String, Container As String) As List(Of SP_ListFunctionalUnitHis_Result)

    ''' <summary>
    ''' Valida el proceso de ingreso en el control cuentas ambulatorios
    ''' </summary>
    ''' <param name="AdmissionNumber"></param>
    ''' <param name="IsCurrentAdmission"></param>
    ''' <param name="Xml"></param>
    ''' <returns></returns>
    Function SP_ProcessAccountControlAmbulatory(AdmissionNumber As String, IsCurrentAdmission As Boolean, Xml As String) As SP_ProcessAccountControlAmbulatory_Result

End Interface
