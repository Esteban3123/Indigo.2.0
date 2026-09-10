'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Kevin Garay Rodrgiuez
' Created          : 07-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IManualConceptsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Graba o Actualiza un concepto manual
    ''' </summary>
    ''' <param name="manualConcept">Concepto Manual</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveManualConcept(ByVal manualConcept As ManualConcepts, ByVal audit As AuditMessage) As ActionMessageResult

    ''' <summary>
    ''' Funcion para obtener un concepto manual
    ''' </summary>
    ''' <param name="Consecutive">Numero de consecutivo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetManualConcepts(ByVal Consecutive As Integer) As ManualConcepts

    ''' <summary>
    ''' Funcion que sirve para verificar un concepto manual ya ha sido registrado a un empleado
    ''' </summary>
    ''' <param name="_conceptId">id del concepto</param>
    ''' <param name="_contractNumber">numero de contrato</param>
    ''' <param name="_listDates">fecha que se quiere iniciar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetManualConceptsByConceptAndDate(_conceptId As Integer, _contractNumber As Integer, _listDates As List(Of Date), ProcessType As Byte, Optional _otherDate As Date = Nothing) As ManualConcepts

    ''' <summary>
    ''' Devuelve si ya hay un concepto manual ya registrado hasta fin de contrato, por concepto y por numero de contraro
    ''' </summary>
    ''' <param name="_conceptId">id del concepto</param>
    ''' <param name="_contractNumber">numero del contrato</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetManualConceptsByConceptAndEndContractTrue(_conceptId As Integer, _contractNumber As Integer, ProcessType As Byte) As Boolean

    Function ValidateManualConceptsMassive(pData As List(Of ImportFileRow)) As List(Of SP_ValidateMassiveManualConcepts_Result)
    Function GetManualConceptsMassive(pData As List(Of ImportFileRow)) As List(Of SP_GetMassiveManualConcepts_Result)
    Sub SaveManualConceptsMassive(pData As List(Of ImportFileRow))

End Interface
