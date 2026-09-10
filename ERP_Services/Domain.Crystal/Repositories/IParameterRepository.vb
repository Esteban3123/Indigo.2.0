'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Juan F. Tamayo
' Created          : 2015-01-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IParameterRepository
    Inherits IRepository(Of CHPARAMET)

    ''' <summary>
    ''' Obtiene los parámetros para un centro de atención
    ''' </summary>
    ''' <param name="attentionCenterCode">Código del centro de atención</param>
    ''' <param name="asNoTracking">Valor que indica si se consulta el parámetro con seguimiento</param>
    ''' <returns>Parámetros para el centro de atención</returns>
    Function GetParameterByAttentionCenter(ByVal attentionCenterCode As String, Optional ByVal asNoTracking As Boolean = True) As CHPARAMET


End Interface
