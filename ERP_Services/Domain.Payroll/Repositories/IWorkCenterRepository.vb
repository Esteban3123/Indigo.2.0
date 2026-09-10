'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 27-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IWorkCenterRepository

    Inherits IRepository(Of WorkCenter)

    ''' <summary>
    ''' Obtiene todos los centros de Trabajo
    ''' </summary>
    ''' <returns>Lista de Compañías</returns>
    Function ListAllWorkCenter() As List(Of WorkCenter)

    ''' <summary>
    ''' Obtiene un Centro de Trabajo determinado
    ''' </summary>
    ''' <param name="code">Codigo del Centro de Trabajo</param>
    ''' <returns>Centro de Trabajo</returns>
    Function GetWorkCenter(ByVal code As String, Optional desatach As Boolean = True) As WorkCenter

    ''' <summary>
    ''' Obtiene un centro de trabajo a través del ID
    ''' </summary>
    ''' <param name="workCenterId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetWorkCenterById(ByVal workCenterId As Integer) As WorkCenter

End Interface
