'***********************************************************************
' Assembly         : Application.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 17-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Public Interface ICommonAdminService


    ''' <summary>
    ''' Consulta los campos nulos para una tabla en un esquema especifico
    ''' </summary>
    ''' <param name="schema">Esquema</param>
    ''' <param name="nameTable">Nombre de la tabla</param>
    ''' <returns>DataSet con el conjunto de campos que son null</returns>
    ''' <remarks></remarks>
    Function GetFieldsNull(company As String, schema As String, nameTable As String) As DataSet

End Interface
