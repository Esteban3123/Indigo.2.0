'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 05-03-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Base
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Domain.Maintenance.Entities

#End Region

Public Interface IMaintenanceParameters
    Inherits IcrudBase

#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene la Descripcion del Campo
    ''' </summary>
    Property DescriptionMaintenanceParameters As String

   
#End Region
End Interface
