'***********************************************************************
' Assembly         : Presentacion.Corporation.MVP
' Author           : Cristhian Mauricio Salazar
' Created          : 08-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base
Imports Domain.Entities
Public Interface IAutoliquidation
    Inherits IcrudBase

    ''' <summary>
    ''' Propiedad para cambiar las enable de los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ActionsOnControls As Boolean

End Interface
