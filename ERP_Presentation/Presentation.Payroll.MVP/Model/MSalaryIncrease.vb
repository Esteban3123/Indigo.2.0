'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 03-07-2015
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

Public Class MSalaryIncrease
    Inherits ModelBase
    Implements IDisposable

    Public Shared TAG As String = "1526"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Lista todos los Grupos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllGroups() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetGroup()
    End Function

    ''' <summary>
    ''' Lista todos las Unidades Funcionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllFunctionalUnit() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetFunctionalUnit()
    End Function

    ''' <summary>
    ''' Lista todos los Cargos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllPosition() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetPosition()
    End Function

    ''' <summary>
    ''' Lista todos las Razones de Modificación de Contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllContractModificationReason() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetContractModificationReason()
    End Function

    ''' <summary>
    ''' Obtiene un ingresos plano por su numero
    ''' </summary>
    Public Function ExecuteIncreaseSalary(ByVal IdGroup As Integer, ByVal IdFunctionalIUnit As Integer, IDPosition As Integer, PercentageIncrease As Decimal, Confirm As Boolean, AproxValue As Decimal, ModificationReasonId As Integer, InitialDateNewSalary As Date, PayrollPaid As Byte, WithRetroactive As Integer, ByVal RetroactiveInitialDate As String, ByVal PayrollPaidRetroactive As Integer) As Task(Of List(Of SP_IncreaseEmployeSalary_Result))
        Dim Res = IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ExecuteIncreaseSalaryAsync(IdGroup, IdFunctionalIUnit, IDPosition, PercentageIncrease, Confirm, AproxValue, ModificationReasonId, InitialDateNewSalary, PayrollPaid, Indigo, WithRetroactive, RetroactiveInitialDate, PayrollPaidRetroactive)

        Return Res
    End Function

    ''' <summary>
    ''' Recibe datos de excel para aumentar salario
    ''' </summary>
    ''' <param name="DataImport"></param>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    Public Function SetIncreaseSalaryFromFile(ByVal DataImport As List(Of ImportFileRow), ByVal Data As List(Of List(Of String))) As List(Of SP_SetIncreaseSalaryFromFile_Result)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SetIncreaseSalaryFromFile(DataImport, Data, Indigo)
    End Function

    ''' <summary>
    ''' Recibe los datos de la rejilla y confirma el aumento de salario
    ''' </summary>
    ''' <param name="IncreaseSalaryData"></param>
    ''' <param name="PercentageIncrease"></param>
    ''' <param name="ModificationReasonId"></param>
    ''' <param name="InitialDateNewSalary"></param>
    ''' <returns></returns>
    Public Function ConfirmIncreaseSalary(ByVal IncreaseSalaryData As List(Of SP_IncreaseEmployeSalary_Result), ByVal PercentageIncrease As Decimal, ByVal ModificationReasonId As Integer, ByVal InitialDateNewSalary As Date) As Task(Of SP_ConfirmIncreaseEmployeSalary_Result)
        Dim Res = IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ConfirmIncreaseEmployeSalaryAsync(IncreaseSalaryData, Indigo.UserIndigo, PercentageIncrease, ModificationReasonId, InitialDateNewSalary, Indigo)
        Return Res
    End Function

    Public Function SaveIncreaseSalary(ByVal contractList As Dynamic.ExpandoObject, ByVal ContractInialDate As Date, ContractModificationReasonId As Integer, Optional ListRetroactiveC As List(Of RetroactiveC) = Nothing) As Task(Of Boolean)
        Dim ObjContractList = Utils.SerializeObjectToJson(contractList)

        Dim res = IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveIncreaseSalaryAsync(ObjContractList, ContractInialDate, ContractModificationReasonId, Indigo, ListRetroactiveC)

        Return res
    End Function

    Public Function ExecuteRetroactive(IdGroup As Integer, IncreasePercentage As Decimal, RetroactiveInitialDate As Date, PayrollPaid As Byte, Optional FunctionalId As Integer = 0, Optional PositionId As Integer = 0, Optional SpecificEmployeeId As Integer = 0) As Task(Of List(Of RetroactiveC))

        Dim res = IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ExecuteRetroactiveAsync(IdGroup, IncreasePercentage, RetroactiveInitialDate, PayrollPaid, Indigo, FunctionalId, PositionId, SpecificEmployeeId)

        Return res
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
