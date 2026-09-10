'***********************************************************************
' Assembly         : Application.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Dynamic
Imports Domain.Crystal.Entities

Public Interface IHCUNITHISAdminService
    Inherits IDisposable

#Region "Methods"
    Function GetHCUNITHISByUFUCODIGO(ufucodigo As String) As List(Of HCUNITHIS)

    Function GetHCUNITHISByUFUCODIGOWithFACMECONINS(ufucodigo As String) As HCUNITHIS
#End Region

End Interface