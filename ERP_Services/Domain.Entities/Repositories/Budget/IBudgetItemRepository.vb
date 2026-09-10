'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 28-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IBudgetItemRepository
    Inherits IRepository(Of Category)

    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <param name="BudgetaryValidityId">Id de la vigencia</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    Function GetBudgetItemByFinancialSourceIdAndCodeAndBudgetaryValidityIdAndItemType(FinancialSourceId As Integer?, Code As String, BudgetaryValidityId As Integer, ItemType As Byte) As Category

    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <param name="BudgetaryValidityId">Id de la vigencia</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    Function GetBudgetItemByCodeAndBudgetaryValidityIdAndItemType(Code As String, BudgetaryValidityId As Integer, ItemType As Byte) As Category

    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <param name="BudgetaryValidityId">Id de la vigencia</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    Function GetBudgetAndFinancialSourceByItemByCodeAndBudgetaryValidityIdAndItemType(Code As String, BudgetaryValidityId As Integer, ItemType As Byte) As Category

    ''' <summary>
    ''' Listado de rubros por vigencia
    ''' </summary>
    ''' <param name="ValidityId">Id de la vigencia</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListBudgetItemsByValidity(ValidityId As String, ItemType As Byte) As List(Of Category)

    ''' <summary>
    ''' funcion para obtener el listado de 
    ''' </summary>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Function GetAllBudgetItemsByState(ByVal state As Boolean) As List(Of Category)

    ''' <summary>
    ''' Devuelve cuantos rubros hay
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CountCategories() As Integer

    ''' <summary>
    ''' consulta un rubro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCategoryById(Id As Integer) As Category

    ''' <summary>
    ''' Obtiene todos los rubros de ingreso=1 o rubros de gasto=2 para copiarlos y agregarlos a otra vigencia
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListCategoryForCopyBase(validityId As Integer, type As Integer) As List(Of Category)

    ''' <summary>
    ''' Obtiene el rubro de ingreso=1 o rubro de gasto=2 por vigencia, tipo (Ingreso, gasto), codigo y codigo de la fuente de financiación
    ''' </summary>
    ''' <param name="validityId"></param>
    ''' <param name="type"></param>
    ''' <param name="code"></param>
    ''' <param name="financialSourceCode"></param>
    ''' <returns></returns>
    Function GetCategoryByCodeAndValidityForCopyBase(validityId As Integer, type As Integer, code As String, financialSourceCode As String) As Category

    ''' <summary>
    ''' Actualizar parametros de presupuesto
    ''' </summary>
    ''' <param name="XmlCriterias"></param>
    ''' <returns></returns>
    Function SP_UpdateParameterizedInformation(XmlCriterias As String) As List(Of SP_UpdateParameterizedInformation_Result)

End Interface
