'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Juan Carlos Bermudez
' Created          : 24/09/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

Public Interface IConfirmationDocumentsExpenses
    Inherits IcrudBase

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object
End Interface
