'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jeisson Herrera Peña
' Created          : 25/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IRecognitionAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Obtiene un reconocimiento por codigo
    ''' </summary>
    '''<param name="Code">Código del reconocimiento</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    Function GetRecognition(Code As String, ItemType As Byte, budgetaryValidityId As Integer, audit As AuditMessage) As ActionResult(Of Recognition)

    ''' <summary>
    ''' Obtiene un reconocimiento por id
    ''' </summary>
    '''<param name="Id">Id del reconocimiento</param>
    ''' <returns></returns>
    Function GetRecognitionById(Id As Integer, audit As AuditMessage) As ActionResult(Of Recognition)

    ''' <summary>
    ''' Guarda o Actualiza un reconocimiento
    ''' </summary>
    ''' <param name="recognition"></param>
    ''' <param name="listDetailsForDelete"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveRecognition(ByVal recognition As Recognition, listDetailsForDelete As List(Of Integer), ByVal audit As AuditMessage) As ActionResult(Of Recognition)

    ''' <summary>
    ''' Elimina un reconocimiento
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteRecognition(ByVal recognition As Recognition, ByVal audit As AuditMessage) As ActionResult

#End Region

End Interface
