'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cesar Augusto Collazos Perdomo
' Created          : 26-09-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IHumanTalentParameterizationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda la parametrización establecida para el formulario de talento humano
    ''' </summary>
    Function SaveHumanTalentParameterization(ByVal _humanTalentParameterization As HumanTalentParameterization, ByVal audit As AuditMessage)

    ''' <summary>
    ''' Obtiene la parametrización  del formulario
    ''' </summary>
    Function GetHumanTalentParameterization() As HumanTalentParameterization
End Interface
