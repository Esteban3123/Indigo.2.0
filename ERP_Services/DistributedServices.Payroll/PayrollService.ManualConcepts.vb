'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 07-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService

    ''' <summary>
    ''' Funcion para obtener un concepto manual
    ''' </summary>
    ''' <param name="Consecutive">Numero de consecutivo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetManualConcepts(Consecutive As Integer, session As SessionValues) As Domain.Payroll.Entities.ManualConcepts Implements IPayrollManualConcepts.GetManualConcepts
        Using manualConcepts As IManualConceptsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IManualConceptsAdminService)()
            Return manualConcepts.GetManualConcepts(Consecutive)
        End Using
    End Function

    ''' <summary>
    ''' Graba o Actualiza un concepto manual
    ''' </summary>
    ''' <param name="manualConcept">Concepto Manual</param>
    ''' <param name="session">Objeto valores de la session</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveManualConcept(manualConcept As Domain.Payroll.Entities.ManualConcepts, session As SessionValues) As Domain.Base.Entities.ActionMessageResult Implements IPayrollManualConcepts.SaveManualConcept
        Using manualConcepts As IManualConceptsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IManualConceptsAdminService)()
            Return manualConcepts.SaveManualConcept(manualConcept, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Funcion que sirve para verificar un concepto manual ya ha sido registrado a un empleado
    ''' </summary>
    ''' <param name="_conceptId">id del concepto</param>
    ''' <param name="_contractNumber">numero de contrato</param>
    ''' <param name="_listDates">fecha que se quiere iniciar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetManualConceptsByConceptAndDate(_conceptId As Integer, _contractNumber As Integer, _listDates As List(Of Date), ProcessType As Byte, session As SessionValues, Optional _otherDate As Date = Nothing) As ManualConcepts Implements IPayrollManualConcepts.GetManualConceptsByConceptAndDate
        Using manualConcepts As IManualConceptsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IManualConceptsAdminService)()
            If _otherDate = Nothing Then
                Return manualConcepts.GetManualConceptsByConceptAndDate(_conceptId, _contractNumber, _listDates, ProcessType)
            Else
                Return manualConcepts.GetManualConceptsByConceptAndDate(_conceptId, _contractNumber, _listDates, ProcessType, _otherDate)
            End If
        End Using
    End Function

    ''' <summary>
    ''' Devuelve si ya hay un concepto manual ya registrado hasta fin de contrato, por concepto y por numero de contraro
    ''' </summary>
    ''' <param name="_conceptId">id del concepto</param>
    ''' <param name="_contractNumber">numero del contrato</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetManualConceptsByConceptAndEndContractTrue(_conceptId As Integer, _contractNumber As Integer, ProcessType As Byte, session As SessionValues) As Boolean Implements IPayrollManualConcepts.GetManualConceptsByConceptAndEndContractTrue
        Using manualConcepts As IManualConceptsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IManualConceptsAdminService)()
            Return manualConcepts.GetManualConceptsByConceptAndEndContractTrue(_conceptId, _contractNumber, ProcessType)
        End Using
    End Function

    Public Function ValidateManualConceptsMassive(pData As List(Of ImportFileRow), pSession As SessionValues) As List(Of SP_ValidateMassiveManualConcepts_Result) Implements IPayrollManualConcepts.ValidateManualConceptsMassive
        Using manualConcepts As IManualConceptsAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IManualConceptsAdminService)()
            Return manualConcepts.ValidateManualConceptsMassive(pData)
        End Using
    End Function

    Function GetManualConceptsMassive(pData As List(Of ImportFileRow), pSession As SessionValues) As List(Of SP_GetMassiveManualConcepts_Result) Implements IPayrollManualConcepts.GetManualConceptsMassive
        Using manualConcepts As IManualConceptsAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IManualConceptsAdminService)()
            Return manualConcepts.GetManualConceptsMassive(pData)
        End Using
    End Function

    Sub SaveManualConceptsMassive(pData As List(Of ImportFileRow), pSession As SessionValues) Implements IPayrollManualConcepts.SaveManualConceptsMassive
        Using manualConcepts As IManualConceptsAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IManualConceptsAdminService)()
            manualConcepts.SaveManualConceptsMassive(pData)
        End Using
    End Sub

End Class
