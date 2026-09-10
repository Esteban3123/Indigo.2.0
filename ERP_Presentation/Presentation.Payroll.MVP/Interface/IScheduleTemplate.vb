'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Cristhian Mauricio Salazar Narvaez
' Created          : 23-07-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Public Interface IScheduleTemplate
    Inherits IcrudBase

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

End Interface
