'***********************************************************************
' Assembly         : Presentacion.MedicalFees.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/01/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Infrastructure.Data.Xpo.MedicalFeesRepository

#End Region

Public Class PMedicalFeesLiquidation

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IMedicalFeesLiquidation

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IMedicalFeesLiquidation)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de los contratos profesionales de la salud
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeMedicalFeesContract()
        View.MedicalFeesContractXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).MedicalFeesService.ListMedicalFeesContractByStatusAndContractType(True, 2)
    End Sub

    ''' <summary>
    ''' Lista las causaciones por contrato o por medico para pagos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewMedicalFeesLiquidationDetailByMedicalFeesLiquidationId(MedicalFeesLiquidationId As Integer) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MedicalFeesService.ListViewMedicalFeesLiquidationDetailByMedicalFeesLiquidationId(MedicalFeesLiquidationId)
    End Function

    ''' <summary>
    ''' Lista detalles de tipo pagos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewMedicalFeesLiquidationPayByMedicalFeesLiquidationId(MedicalFeesLiquidationId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MedicalFeesService.ListViewMedicalFeesLiquidationPayByMedicalFeesLiquidationId(MedicalFeesLiquidationId)
    End Function

    ''' <summary>
    ''' Lista las causaciones por contrato o por medico para pagos con un XpInstanFeedBackSource
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCausationByMedicalFeesContractIdViewXpoXpInstantFeedBackSource(listId As List(Of Integer), medicalFeesContractId As Integer?, initialDate As DateTime, endDate As DateTime, healthProfessionalCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MedicalFeesService.ListCausationByMedicalFeesContractIdViewXpoXpInstantFeedBackSource(listId, medicalFeesContractId, initialDate, endDate, healthProfessionalCode)
    End Function

    ''' <summary>
    ''' Inicializa el datasource de las causaciones por contrato
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeMedicalFeesCausation(ByVal medicalFeesContractId As Integer)
        Using msearch As New MBusqueda
            'View.MedicalFeesCausationXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMedicalFeesCausationByMedicalFeesContractId, medicalFeesContractId)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de unidad de radicacion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeFilingUnit()
        Using model As New MBusqueda
            Me.View.FilingUnitXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFilingUnitByStatusCollection, True)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el tipo de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeSupplierType()
        Using model As New MBusqueda
            Me.View.SupplierTypeXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSupplierTypeByStatusTreeList, True)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa los medicos
    ''' </summary>
    ''' <param name="medicalFeesContractId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeHealthProfessional(medicalFeesContractId As Integer)
        Using model As New MBusqueda
            Me.View.HealthProfessionalXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListHealthProfessionalCodeByMedicalFeesContractId, medicalFeesContractId)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Sub InitializeCostCenter()
        'Using model As New MBusqueda
        Me.View.CostCenterXpo = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True)'model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
        'End Using
    End Sub

    ''' <summary>
    ''' lista los profesionales de la salud
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeHealthProfessional()
        Me.View.HealthProfessionalXpo = XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListHealthCareProfessional()
    End Sub

    Public Function GetPatient(CodePatient As String) As List(Of PatientXpo)
        Dim filtroConsulta2 As String = "IPCODPACI = '" & CodePatient & "'"
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCollection(Of PatientXpo)(Nothing, filtroConsulta2)
    End Function

    ''' <summary>
    ''' Obtiene el contrato por id
    ''' </summary>
    ''' <param name="MedicalFeesContractId"></param>
    ''' <returns></returns>
    Public Function GetMedicalFeesContract(MedicalFeesContractId As Integer) As MedicalFeesContractXpo
        Dim filtroConsulta As String = "Id = '" & MedicalFeesContractId & "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MedicalFeesService.GetCollection(Of MedicalFeesContractXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene el contrato por id de manera asíncrona (NO bloquea la UI)
    ''' </summary>
    ''' <param name="MedicalFeesContractId"></param>
    ''' <returns></returns>
    Public Async Function GetMedicalFeesContractAsync(MedicalFeesContractId As Integer) As Task(Of MedicalFeesContractXpo)
        Return Await Task.Run(Function()
                                  Dim filtroConsulta As String = "Id = '" & MedicalFeesContractId & "'"
                                  Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MedicalFeesService.GetCollection(Of MedicalFeesContractXpo)(Nothing, filtroConsulta).FirstOrDefault()
                              End Function)
    End Function

#End Region

End Class
