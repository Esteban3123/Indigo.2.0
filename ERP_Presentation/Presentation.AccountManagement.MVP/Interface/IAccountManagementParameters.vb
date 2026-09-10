'***********************************************************************
' Assembly         : Presentacion.AccoungManagement
' Author           : Andres Felipe Quintero Garcia 
' Created          : 09-01-2025
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Import"
Imports Presentation.Base
Imports Presentation.Controls
#End Region

Public Interface IAccountManagementParameters
    Inherits ICrudBase

    Property ValidateAutomaticAssignment As Boolean
    'Property EntryType As String
    Property StartDateAssignment As DateTime?
    'Property BedClass As String
    ReadOnly Property MyLayoutControl As IndigoLayoutControl
    'WriteOnly Property ActionsOnControls As Boolean
End Interface
