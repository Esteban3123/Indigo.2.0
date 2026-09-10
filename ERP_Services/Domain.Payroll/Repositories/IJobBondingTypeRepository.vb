'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IJobBondingTypeRepository
    Inherits IRepository(Of JobBondingType)

    ''' <summary>
    ''' Lista Todas las Tipos de Vinculación Laboral
    ''' </summary>
    ''' <returns>Tipos de Vinculación Laboral</returns>
    ''' <remarks></remarks>
    Function ListAllJobBondingType() As List(Of JobBondingType)

    ''' <summary>
    ''' Obtiene un Tipo de Vinculación Laboral
    ''' </summary>
    ''' <param name="code">Código de la Vinculación Laboral</param>
    ''' <returns>Tipo de Vinculación Laboral</returns>
    ''' <remarks></remarks>
    Function GetJobBondingType(ByVal code As String, Optional tracking As Boolean = True) As JobBondingType

End Interface
