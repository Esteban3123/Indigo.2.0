Imports System.Runtime.Serialization

Partial Public Class CopyBase

    ''' <summary>
    ''' Obtiene o establece el id de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property BudgetEntityIdSource As Integer?

    ''' <summary>
    ''' Obtiene o establece el año de la vigencia de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property ValidityIdSource As Integer?

    ''' <summary>
    ''' Obtiene o establece el id de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property BudgetEntityIdDestiny As Integer?

    ''' <summary>
    ''' Obtiene o establece el año de la vigencia de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property ValidityIdDestiny As Integer?

    ''' <summary>
    ''' Recursos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property Resource As Boolean?

    ''' <summary>
    ''' Dependencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property Dependency As Boolean?

    ''' <summary>
    ''' Conceptos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property Concepts As Boolean?

    ''' <summary>
    ''' Tipos de Ingreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property IncomeType As Boolean?

    ''' <summary>
    ''' Tipos de Gasto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property ExpenseType As Boolean?

    ''' <summary>
    ''' Rubros de Ingresos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property CategoryIncome As Boolean?

    ''' <summary>
    ''' Rubros de Gastos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property CategoryExpense As Boolean?

    ''' <summary>
    ''' Actualizar dependencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property UpdateParameterizedDependencies As Boolean?

    ''' <summary>
    ''' Actualizar rubros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property UpdateParameterizedCategories As Boolean?

    ''' <summary>
    ''' Listado de errores que devuelve el copiado del presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property ListErrors As List(Of Tuple(Of String, Integer))

End Class
