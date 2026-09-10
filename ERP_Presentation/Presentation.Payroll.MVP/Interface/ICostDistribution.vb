'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 28-07-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports Domain.Payroll.Entities
#End Region

Public Interface ICostDistribution
    Inherits IcrudBase
    ''' <summary>
    ''' Establece el datasource de los grupos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property Datasource_Groups As Object
End Interface
