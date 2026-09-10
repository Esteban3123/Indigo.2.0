'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Steven Rojas Rodriguez
' Created          : 26-10-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Payroll.Entities
Imports Domain.Base
Public Interface ILicensingConceptsRepository
    Inherits IRepository(Of LicensingConcepts)

    ''' <summary>
    ''' Lista todos los conceptos de licencia 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllLicensingConcepts() As List(Of LicensingConcepts)

    ''' <summary>
    ''' Obtiene un concepto de licencia especifico
    ''' </summary>
    ''' <param name="code">Codigo del Banco</param>
    ''' <returns>Banco</returns>
    ''' <remarks></remarks>
    Function GetLicensingConcepts(ByVal code As String, Optional tracking As Boolean = True) As LicensingConcepts

    ''' <summary>
    ''' Obtiene un Concepto de licencia por el identificador
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetLicensingConceptsById(ByVal Id As Integer, Optional tracking As Boolean = True) As LicensingConcepts
End Interface
