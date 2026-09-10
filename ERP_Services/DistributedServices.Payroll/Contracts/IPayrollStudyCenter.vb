'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollStudyCenter

    ''' <summary>
    ''' Lista todos los Centros de Estudio
    ''' </summary>
    ''' <returns>Centros de Estudio</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllStudyCenter(session As SessionValues) As List(Of StudyCenter)

    ''' <summary>
    ''' Elimina un Centro de Estudio
    ''' </summary>
    ''' <param name="studyCenter">Centro de Estudio</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteStudyCenter(ByVal studyCenter As StudyCenter, session As SessionValues) As ActionMessageResult(Of StudyCenter)

    ''' <summary>
    ''' Actualiza o Almacena un Centro de Estudio
    ''' </summary>
    ''' <param name="studyCenter">Centro de Estudio</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveStudyCenter(ByVal studyCenter As StudyCenter, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene un Centro de Estudio
    ''' </summary>
    ''' <param name="code">Código del Centro de Estudio</param>
    ''' <returns>Centro de Estudio</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetStudyCenter(ByVal code As String, session As SessionValues) As StudyCenter

End Interface
