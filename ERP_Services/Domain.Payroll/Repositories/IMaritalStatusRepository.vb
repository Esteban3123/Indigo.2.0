' ***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Antony F. Córdoba P.
' Created          : 20-12-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IMaritalStatusRepository
    Inherits IRepository(Of MaritalStatus)

    ''' <summary>
    ''' Lista todos los tipos de estado civil
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllMaritalStatus() As List(Of MaritalStatus)

    ''' <summary>
    ''' Obtiene por codigo el tipo de estado civil
    ''' </summary>
    ''' <param name="Code">codigo </param>
    ''' <remarks></remarks>
    Function GetMaritalStatusByCode(ByVal Code As String, Optional tracking As Boolean = True) As MaritalStatus

    ''' <summary>
    ''' Obtiene por id el tipo de estado civil
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetMaritalStatusById(ByVal Id As Integer, Optional tracking As Boolean = True) As MaritalStatus

End Interface