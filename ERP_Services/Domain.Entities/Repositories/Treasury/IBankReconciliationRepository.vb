#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IBankReconciliationRepository
    Inherits IRepository(Of BankReconciliation)

    ''' <summary>
    ''' Obtiene una conciliación por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBankReconciliationById(id As Integer, Optional tracking As Boolean = True) As BankReconciliation

    ''' <summary>
    ''' Obtiene una conciliación por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBankReconciliationByCode(code As String, Optional tracking As Boolean = True) As BankReconciliation

    ''' <summary>
    ''' Obtiene los detalles de una conciliación
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Function SP_GetBankReconciliationDetails(XmlCriterias As String) As List(Of SP_GetBankReconciliationDetails_Result)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Function GenerateBankReconciliationSP(xml As String, UserCode As String) As Entity.Core.Objects.ObjectResult(Of SP_SaveBankReconciliation_Result)

End Interface
