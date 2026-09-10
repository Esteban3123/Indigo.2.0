
'************************************************************
' Assembly         : Domain.Maintenance
' Author           : Oscar Ivan Sierra
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************



#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
Imports Domain.Maintenance.Entities
#End Region



''' <summary>
''' clase para definir cada una de la spropiedades y metodos que se van a persistir en la clase del repositorio
''' </summary>
''' <remarks></remarks>

Public Interface IPartRepository
    Inherits IRepository(Of Part)

    ''' <summary>
    ''' funcion que lista todas los partes
    ''' </summary>
    ''' <returns>Lista de partes</returns>
    Function ListAllPart() As List(Of Part)
    ''' <summary>
    ''' consulta para retornar una parte teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codePart">el codigo de la parte</param>
    ''' <returns>Objeto Compañia</returns>
    Function GetPart(ByVal codePart As String, Optional ByVal tracking As Boolean = True) As Part

    ''' <summary>
    ''' funcion para almacenar una parte
    ''' </summary>
    ''' <param name="Part"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePart(Part As Part) As Boolean
End Interface

