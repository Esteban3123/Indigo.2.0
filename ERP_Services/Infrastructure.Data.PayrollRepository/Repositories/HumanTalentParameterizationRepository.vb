'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cesar Augusto Collazos Perdomo
' Created          : 26-09-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
#End Region

Public Class HumanTalentParameterizationRepository
    Inherits GenericRepository(Of HumanTalentParameterization)
    Implements IHumanTalentParameterizationRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Función que obtiene el registro que contiene la parametrización del JSON
    ''' </summary>
    ''' <returns></returns>
    Public Function GetHumanTalentParameterization() As HumanTalentParameterization Implements IHumanTalentParameterizationRepository.GetHumanTalentParameterization
        Dim JSONparameterization = (From e In _context.HumanTalentParameterization).FirstOrDefault()
        Return JSONparameterization
    End Function

End Class
