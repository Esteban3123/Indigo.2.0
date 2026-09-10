Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.Registrator
Imports System.ComponentModel
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid

Public Class GridLookUpMultiFilter
    Inherits GridLookUpEdit

    Shared Sub New()
        RepositoryItemCustomGridLookUpEdit.RegisterCustomGridLookUpEdit()
    End Sub

    Public Overrides ReadOnly Property EditorTypeName() As String
        Get
            Return RepositoryItemCustomGridLookUpEdit.GridLookUpMultiFilter
        End Get
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    Public Shadows ReadOnly Property Properties() As RepositoryItemCustomGridLookUpEdit
        Get
            Return TryCast(MyBase.Properties, RepositoryItemCustomGridLookUpEdit)
        End Get
    End Property

End Class

<UserRepositoryItem("RegisterCustomGridLookUpEdit")>
Public Class RepositoryItemCustomGridLookUpEdit
    Inherits RepositoryItemGridLookUpEdit

    Shared Sub New()
        RegisterCustomGridLookUpEdit()
    End Sub

    Public Sub New()
        TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        AutoComplete = False
    End Sub
    <Browsable(False)> _
    Public Overrides Property TextEditStyle() As DevExpress.XtraEditors.Controls.TextEditStyles
        Get
            Return MyBase.TextEditStyle
        End Get
        Set(value As DevExpress.XtraEditors.Controls.TextEditStyles)
            MyBase.TextEditStyle = value
        End Set
    End Property
    Public Const GridLookUpMultiFilter As String = "GridLookUpMultiFilter"

    Public Overrides ReadOnly Property EditorTypeName() As String
        Get
            Return GridLookUpMultiFilter
        End Get
    End Property

    Public Shared Sub RegisterCustomGridLookUpEdit()
        EditorRegistrationInfo.[Default].Editors.Add(New EditorClassInfo(GridLookUpMultiFilter, GetType(GridLookUpMultiFilter), GetType(RepositoryItemCustomGridLookUpEdit), GetType(GridLookUpEditBaseViewInfo), New ButtonEditPainter(), True))
    End Sub

    'Protected Overrides Function CreateViewInstance() As GridView
    'Return New CustomGridView()
    'End Function
    Protected Overrides Function CreateGrid() As GridControl
        Return New GridControlMultiFilter()
    End Function

End Class
