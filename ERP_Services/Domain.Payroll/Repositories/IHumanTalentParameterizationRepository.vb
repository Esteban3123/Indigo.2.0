'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cesar Augusto Collazos Perdomo
' Created          : 26-09-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Payroll.Entities
#End Region

Public Interface IHumanTalentParameterizationRepository
    Inherits IRepository(Of HumanTalentParameterization)

    ''' <summary>
    ''' Obtiene la parametrizacion del formulario de talento humano
    ''' </summary>
    ''' <returns></returns>
    Function GetHumanTalentParameterization() As HumanTalentParameterization
End Interface
