'***********************************************************************
' Assembly         : Presentation.Security.MVP
' Author           : Jhon Tovar
' Created          : 10-03-2022
'
' Last Modified By :
' Last Modified On :
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base.Entities
Imports Domain.Base.Entities.Core.Enums
Imports Presentation.Base
Imports Presentation.Controls

Public Interface IProductCatalog
    Inherits ICrudBase

    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    Property IdProduct As Integer
    Property ProductName As String
    Property PlatformName As String
    Property SuiteName As String
    Property State As Boolean
    Property Visible As Byte?
    Property Crud As ECrud

End Interface
