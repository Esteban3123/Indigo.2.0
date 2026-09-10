'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 07-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class ManualConceptsRepository

    Inherits GenericRepository(Of ManualConcepts)
    Implements IManualConcepts

    'Contexto de payroll
    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Funcion para obtener un concepto manual
    ''' </summary>
    ''' <param name="Consecutive">Numero de consecutivo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetManualConcepts(ByVal Consecutive As Integer) As ManualConcepts Implements IManualConcepts.GetManualConcepts
        Dim manualConcepts = From e In _context.ManualConcepts.Include("ManualConceptsDetail").Include("Contract").Include("Contract.Employee") _
                             .Include("Contract.Employee.ThirdParty").Include("Group").Include("Concept").Include("FunctionalUnit").Include("BranchOffice") _
                             .Include("CostCenter").Include("Group.Company").Include("Contract.Position")
                            Where e.Consecutive = Consecutive
                            Select e

        If manualConcepts.Count > 0 Then
            manualConcepts.FirstOrDefault().ManualConceptsAux = (From e In _context.ManualConcepts.AsNoTracking
                                   Where e.Consecutive = Consecutive
                                   Select e).SingleOrDefault
            Return manualConcepts.SingleOrDefault
        Else
            Return New ManualConcepts
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene Los conceptos manuales por Número de Contrato, Fecha del Pago de la Nómina y que su estado sea ACTIVO
    ''' </summary>
    ''' <param name="ContractNumber">Número del Contrato</param>
    ''' <param name="PayrollInitialDate">Fecha Inicio Nómina</param>
    ''' <param name="PayrollEndDate">Fecha Fin Nómina</param>
    ''' <param name="State">Estado del Concepto Manual</param>
    ''' <returns>Manual Concept</returns>
    ''' <remarks></remarks>
    Public Function GetManualConceptsByContractNumberPayrollDate(ByVal ContractNumber As Integer, ByVal PayrollInitialDate As Date, PayrollEndDate As Date, State As Byte, ProcessType As Byte, Optional FlagSave As Boolean = False) As List(Of ManualConcepts) Implements IManualConcepts.GetManualConceptsByContractNumberPayrollDate

        Dim ListReturn As New List(Of ManualConcepts)

        If FlagSave = True Then
            Dim FijomanualConcepts = From e In _context.ManualConcepts.Include("ManualConceptsDetail").Include("Concept")
                                     Where e.ContractNumber = ContractNumber And e.State = State And e.PayrollEndingDate >= PayrollInitialDate And e.PayrollInitialDate <= PayrollEndDate And e.PaidEndContract = False And e.Process = ProcessType
                                     Select e

            Dim EndContractmanualConcepts = From e In _context.ManualConcepts.Include("ManualConceptsDetail").Include("Concept")
                                            Where e.ContractNumber = ContractNumber And e.State = State And e.PaidEndContract = True And PayrollEndDate >= e.InitialDate And e.Process = ProcessType
                                            Select e

            If FijomanualConcepts.Count > 0 Then
                ListReturn.AddRange(FijomanualConcepts.ToList())
            End If


            If EndContractmanualConcepts.Count > 0 Then
                ListReturn.AddRange(EndContractmanualConcepts.ToList())
            End If
        Else
            Dim FijomanualConcepts = From e In _context.ManualConcepts.AsNoTracking.Include("ManualConceptsDetail").AsNoTracking.Include("Concept").AsNoTracking
                                     Where e.ContractNumber = ContractNumber And e.State = State And e.PayrollEndingDate >= PayrollInitialDate And e.PayrollInitialDate <= PayrollEndDate And e.PaidEndContract = False And e.Process = ProcessType
                                     Select e

            Dim EndContractmanualConcepts = From e In _context.ManualConcepts.AsNoTracking.Include("ManualConceptsDetail").AsNoTracking.Include("Concept").AsNoTracking
                                            Where e.ContractNumber = ContractNumber And e.State = State And e.PaidEndContract = True And PayrollEndDate >= e.InitialDate And e.Process = ProcessType
                                            Select e

            Dim listIdsPaiedConcept = (From e In _context.ManualConceptsDetail.AsNoTracking.Include("ManualConcepts")
                                       Where e.ManualConcepts.ContractNumber = ContractNumber And e.State = 2 And e.PayrollDateLiquidated >= PayrollInitialDate And e.PayrollDateLiquidated <= PayrollEndDate
                                       Select e.ManualConceptId).ToList()

            If FijomanualConcepts.Count > 0 Then
                ListReturn.AddRange(FijomanualConcepts.ToList())
            End If


            If EndContractmanualConcepts.Count > 0 Then
                ListReturn.AddRange(EndContractmanualConcepts.ToList())
            End If

            If listIdsPaiedConcept?.Count > 0 Then
                Dim paiedConcepts = From e In _context.ManualConcepts.AsNoTracking.Include("ManualConceptsDetail").AsNoTracking.Include("Concept").AsNoTracking
                                    Where listIdsPaiedConcept.Contains(e.Id)
                                    Select e
                ListReturn.AddRange(paiedConcepts.ToList())
            End If
        End If


        If ListReturn IsNot Nothing AndAlso ListReturn.Count > 0 Then
            Return ListReturn
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Funcion que sirve para verificar un concepto manual ya ha sido registrado a un empleado
    ''' </summary>
    ''' <param name="_conceptId">id del concepto</param>
    ''' <param name="_contractNumber">numero de contrato</param>
    ''' <param name="_listDates">listado de fechas que se quieren verificar</param>
    ''' <param name="_date">variable opcional para indicar si se valida apartir de una fecha</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetManualConceptsByConceptAndDate(_conceptId As Integer, _contractNumber As Integer, _listDates As List(Of Date), ProcessType As Byte, Optional _otherDate As Date = Nothing) As ManualConcepts Implements IManualConcepts.GetManualConceptsByConceptAndDate
        If _otherDate = Nothing Then
            For Each _date In _listDates
                Dim manualConcept = From e In _context.ManualConcepts
                                    Where e.State = 1 And e.ConceptId = _conceptId And e.ContractNumber = _contractNumber And _date >= e.PayrollInitialDate And _date <= e.PayrollEndingDate And e.Process = ProcessType
                                    Select e

                If manualConcept.Count > 0 Then
                    Return manualConcept.FirstOrDefault()
                End If
            Next
        Else
            Dim manualConcept = From e In _context.ManualConcepts
                                   Where e.State = 1 And e.ConceptId = _conceptId And e.ContractNumber = _contractNumber And e.PayrollInitialDate >= _otherDate And e.Process = ProcessType
                                   Select e

            If manualConcept.Count > 0 Then
                Return manualConcept.FirstOrDefault()
            End If
        End If

        Return New ManualConcepts()
    End Function

    ''' <summary>
    ''' Devuelve si ya hay un concepto manual ya registrado hasta fin de contrato, por concepto y por numero de contraro
    ''' </summary>
    ''' <param name="_conceptId">id del concepto</param>
    ''' <param name="_contractNumber">numero del contrato</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetManualConceptsByConceptAndEndContractTrue(_conceptId As Integer, _contractNumber As Integer, ProcessType As Byte) As Boolean Implements IManualConcepts.GetManualConceptsByConceptAndEndContractTrue
        Dim manualConcept = From e In _context.ManualConcepts
                            Where e.ConceptId = _conceptId And e.ContractNumber = _contractNumber And e.PaidEndContract = True And e.State = 1 And e.Process = ProcessType
                            Select e

        If manualConcept.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene Los conceptos manuales por Número de Contrato, Fecha del Pago de la Nómina y que su estado sea ACTIVO
    ''' </summary>
    ''' <param name="ContractNumber">Número del Contrato</param>
    ''' <param name="PayrollInitialDate">Fecha Inicio Nómina</param>
    ''' <param name="PayrollEndDate">Fecha Fin Nómina</param>
    ''' <param name="State">Estado del Concepto Manual</param>
    ''' <returns>Manual Concept</returns>
    ''' <remarks></remarks>
    Public Function GetManualConceptsByEmployeeIdInitialDate(ByVal EmployeeId As Integer, ByVal InitialDate As Date, State As Byte, ProcessType As Byte) As List(Of ManualConcepts) Implements IManualConcepts.GetManualConceptsByEmployeeIdInitialDate

        Dim ListReturn As New List(Of ManualConcepts)

        Dim FijomanualConcepts = From e In _context.ManualConcepts.Include("ManualConceptsDetail").Include("Concept")
                                 Where e.EmployeeId = EmployeeId And e.State = State And e.InitialDate = InitialDate And e.PaidEndContract = False And e.Process = ProcessType
                                 Select e

        Dim EndContractmanualConcepts = From e In _context.ManualConcepts.Include("ManualConceptsDetail").Include("Concept")
                                        Where e.EmployeeId = EmployeeId And e.State = State And e.InitialDate = InitialDate And e.Process = ProcessType And e.PaidEndContract = True
                                        Select e

        If FijomanualConcepts.Count > 0 Then
            ListReturn.AddRange(FijomanualConcepts.ToList())
        End If


        If EndContractmanualConcepts.Count > 0 Then
            ListReturn.AddRange(EndContractmanualConcepts.ToList())
        End If


        If ListReturn IsNot Nothing AndAlso ListReturn.Count > 0 Then
            Return ListReturn
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene el listado de Conceptos Manuales por Id de Contrato
    ''' </summary>
    ''' <param name="IdContract">IdContract</param>
    ''' <returns> List(Of ManualConcepts)</returns>
    Public Function GetManualConceptsContractId(ByVal IdContract As Integer) As List(Of ManualConcepts) Implements IManualConcepts.GetManualConceptsContractId
        Dim manualConcepts = From e In _context.ManualConcepts
                             Where e.ContractId = IdContract
                             Select e

        Return manualConcepts.ToList()
    End Function

    Public Function GetManualConceptsByLiquidationContract(IdEmployee As Integer, Status As Byte) As List(Of ManualConcepts) Implements IManualConcepts.GetManualConceptsByLiquidationContract
        Dim manualConcepts = From e In _context.ManualConcepts.Include("ManualConceptsDetail")
                             Where e.EmployeeId = IdEmployee And e.State = Status
                             Select e

        Return manualConcepts.ToList()
    End Function

    Public Function ValidateManualConceptsMassive(pXMLObj As String) As List(Of SP_ValidateMassiveManualConcepts_Result) Implements IManualConcepts.ValidateManualConceptsMassive
        Return _context.SP_ValidateMassiveManualConcepts(pXMLObj).ToList()
    End Function

    Public Function GetManualConceptsMassive(pXMLObj As String) As List(Of SP_GetMassiveManualConcepts_Result) Implements IManualConcepts.GetManualConceptsMassive
        Return _context.SP_GetMassiveManualConcepts(pXMLObj).ToList()
    End Function

    Public Sub SaveManualConceptsMassive(pXMLObj As String) Implements IManualConcepts.SaveManualConceptsMassive
        _context.SP_SaveMassiveManualConcepts(pXMLObj)
    End Sub
End Class
