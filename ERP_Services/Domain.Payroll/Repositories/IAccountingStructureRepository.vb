'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 22-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IAccountingStructureRepository

    Inherits IRepository(Of AccountingStructure)

    ''' <summary>
    ''' Obtiene la Estructura Contable de Nómina x Código
    ''' </summary>
    ''' <param name="code">Código Accounting Structure</param>
    ''' <param name="desatach"></param>
    ''' <returns>AccountingStructure</returns>
    ''' <remarks></remarks>
    Function GetAccountingStructure(ByVal code As String, Optional desatach As Boolean = True) As AccountingStructure

    ''' <summary>
    ''' Obtiene la Estructura Contable de Nómina x Id
    ''' </summary>
    ''' <param name="accountingStructureId">Id Accounting Structure</param>
    ''' <returns>AccountingStructure</returns>
    ''' <remarks></remarks>
    Function GetAccountingStructureById(ByVal accountingStructureId As Integer, Optional desatach As Boolean = True) As AccountingStructure

    ''' <summary>
    ''' Lista Toda las Estructuras Contables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllAccountingStructure() As List(Of AccountingStructure)

End Interface
