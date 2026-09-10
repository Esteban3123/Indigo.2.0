'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 11-12-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
'Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Presentation.Base

#End Region

''' <summary>
''' Clase que contiene el modelo del formulario para comunicarse con los servicios
''' </summary>
''' <remarks></remarks>
Public Class MUnemployedLiquidation
    Inherits ModelBase
    Implements IDisposable


#Region "Properties"

    Public Shared TAG As String = "556"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

#End Region

#Region "methods"

    ''' <summary>
    ''' Metodo para liquidar las cesantías anuales
    ''' </summary>
    ''' <param name="list_grupos">listado de grupos</param>
    ''' <param name="InitialDate">Fecha Inicio liquidación de cesantías</param>
    ''' <param name="EndingDate">Fecha Fin liquidación de cesantías</param>
    ''' <param name="confim">si va confirmada la liquidación</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function UnemployedLiquidationAsync(EmployeeToLiquidate As Employee, list_grupos As List(Of Group), InitialDate As Date, EndingDate As Date, Confim As Boolean, Sanction As Boolean, Optional AuthorizationDate As Date = Nothing, Optional UnemployedRetirementReason As String = "", Optional ResolutionNumber As String = "", Optional UnemployedInterestPaidWithPayroll As Boolean = 0) As Task(Of ActionMessageResult(Of List(Of UnemployedLiquidation)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.UnemployedLiquidationAsync(EmployeeToLiquidate, list_grupos, InitialDate, EndingDate, Confim, Sanction, Indigo, AuthorizationDate, UnemployedRetirementReason, ResolutionNumber, UnemployedInterestPaidWithPayroll)
    End Function

    ''' <summary>
    ''' Metodo que consulta las liquidaciones de grupos o empleados
    ''' </summary>
    ''' <param name="EmployeeToConsult">empleado a consultar</param>
    ''' <param name="ListGroups">listado de grupos a consultar</param>
    ''' <param name="Period">periodo</param>
    ''' <returns>los registros de liquidación de cesantías</returns>
    ''' <remarks></remarks>
    Public Async Function ConsultUnemployedLiquidationAsync(EmployeeToConsult As Employee, ListGroups As List(Of Group), Optional Period As Integer = 0) As Task(Of List(Of UnemployedLiquidation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ConsultUnemployedLiquidationAsync(EmployeeToConsult, ListGroups, Indigo, Period)
    End Function

    Public Async Function GetLiquidationDetailByContractIdConceptClass(ByVal Year As Integer, InitialContractNumber As Integer, conceptClass As String) As Task(Of List(Of LiquidationDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetLiquidationDetailByContractIdConceptClassAsync(Year, InitialContractNumber, conceptClass, Indigo)
    End Function

    Public Async Function GetLiquidationDetailByContractIdConceptAffectUnemployement(ByVal Year As Integer, InitialContractNumber As Integer, AffectUnemployement As Boolean) As Task(Of List(Of LiquidationDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetLiquidationDetailByContractIdConceptAffectUnemployementAsync(Year, InitialContractNumber, AffectUnemployement, Indigo)
    End Function

    Public Async Function ConfirmUnemployment(ByVal ListUnemployment As List(Of UnemployedLiquidation)) As Task(Of ActionResult(Of List(Of UnemployedLiquidation)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ConfirmUnemploymentAsync(ListUnemployment, Indigo)
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
