'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/03/2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports DevExpress.Xpo
#End Region

Public Interface ICampaign
    Inherits ICrudBase

    ''' <summary>
    ''' Obtiene o asigna los permisos que el usuario tiene asignados en éste formulario
    ''' </summary>
    ''' <value>Diccionario de permisos del usuario</value>
    ''' <returns>El diccionario de permisos del usuario</returns>
    Property PermissionsForm As Dictionary(Of Integer, String)

End Interface
