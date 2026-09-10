
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
Public Interface IResponsibleRepository
    Inherits IRepository(Of Responsible)

    ''' <summary>
    ''' funcion que lista todas los responsables
    ''' </summary>
    ''' <returns>Lista de responsables</returns>
    Function ListAllResponsible() As List(Of Responsible)
    ''' <summary>
    ''' consulta para retornar un responsable teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codeResponsible">el codigo del responsible</param>
    ''' <returns>Objeto responsable</returns>
    Function GetResponsible(ByVal codeResponsible As String, Optional tracking As Boolean = True) As Responsible

    ''' <summary>
    ''' funcion para almacenar una Responsible
    ''' </summary>
    ''' <param name="Responsible"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveResponsible(Responsible As Responsible) As Boolean
End Interface
