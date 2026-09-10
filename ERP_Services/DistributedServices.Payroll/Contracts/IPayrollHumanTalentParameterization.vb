'***********************************************************************
' Author           : Cesar Collazos
' Created          : 09-02-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
#End Region
<ServiceContract()>
    Public Interface IPayrollHumanTalentParameterization

    ''' <summary>
    ''' Obtiene la parametrización del formulario de talento humano
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetHumanTalentParameterization(session As SessionValues) As HumanTalentParameterization
    ''' <summary>
    ''' Guarda/Actualiza la parametrización de talento humano
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveHumanTalentParameterization(ByVal HumanTalentParameterization As HumanTalentParameterization, session As SessionValues)
End Interface
