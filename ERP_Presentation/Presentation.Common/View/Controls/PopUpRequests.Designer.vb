Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PopUpRequests
    Inherits Presentation.Controls.FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygGeneralInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciPacient = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDTePacient = New DevExpress.XtraEditors.TextEdit()
        Me.INDlyAuthorization = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTeStatus = New DevExpress.XtraEditors.TextEdit()
        Me.INDGleAuthorized = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.INDGvAuthorized = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvAuthorized_Description = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleQuoted = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.INDGvQuoted = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvQuoted_Description = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleContracted = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.INDGvContracted = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvContracted_Description = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleCovered = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.INDGvCovered = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvCovered_Description = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleFinancedResourceUPC = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.INDGvFinancedResourceUPC = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvFinancedResourceUPC_Description = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.INDGvType = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvType_Description = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTeQuantity = New DevExpress.XtraEditors.TextEdit()
        Me.INDTeItem = New DevExpress.XtraEditors.TextEdit()
        Me.INDTeProfessional = New DevExpress.XtraEditors.TextEdit()
        Me.INDTeHealthAdministrator = New DevExpress.XtraEditors.TextEdit()
        Me.INDTeCareGroup = New DevExpress.XtraEditors.TextEdit()
        Me.INDTeFunctionalUnit = New DevExpress.XtraEditors.TextEdit()
        Me.INDTeCareCenter = New DevExpress.XtraEditors.TextEdit()
        Me.INDTeFolio = New DevExpress.XtraEditors.TextEdit()
        Me.INDDeRequestDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDTeAdmissionNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDLciAdmissionNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCareCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciFunctionalUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCareGroup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciHealthAdministrator = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciFolio = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygAuthorizationInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCovered = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciContracted = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciQuoted = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAuthorized = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciStatus = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygAditionalInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciRequestDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciProfessional = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciItem = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciFinancedResourceUPC = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn375 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn376 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn377 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn372 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn373 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn374 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn369 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn370 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn371 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn366 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn367 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn368 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn363 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn364 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn365 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn362 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn360 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn358 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn315 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn316 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn317 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn318 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn319 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn320 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn321 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn322 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn323 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn324 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn325 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn326 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn327 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn328 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn329 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn330 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn331 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn332 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn333 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn334 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn335 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn336 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn337 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn338 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn339 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn340 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn341 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn342 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn343 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn344 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn345 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn346 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn347 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn348 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn349 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn350 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn351 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn352 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn353 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn354 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn275 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn276 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn277 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn278 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn279 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn280 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn281 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn282 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn283 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn284 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn285 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn286 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn287 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn288 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn289 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn290 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn291 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn292 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn293 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn294 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn295 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn296 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn297 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn298 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn299 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn300 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn301 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn302 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn303 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn304 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn305 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn306 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn307 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn308 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn309 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn310 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn311 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn312 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn313 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn314 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn235 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn236 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn237 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn238 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn239 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn240 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn241 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn242 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn243 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn244 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn245 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn246 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn247 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn248 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn249 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn250 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn251 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn252 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn253 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn254 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn255 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn256 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn257 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn258 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn259 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn260 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn261 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn262 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn263 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn264 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn265 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn266 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn267 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn268 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn269 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn270 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn271 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn272 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn273 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn274 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn195 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn196 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn197 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn198 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn199 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn200 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn201 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn202 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn203 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn204 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn205 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn206 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn207 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn208 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn209 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn210 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn211 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn212 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn213 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn214 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn215 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn216 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn217 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn218 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn219 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn220 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn221 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn222 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn223 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn224 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn225 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn226 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn227 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn228 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn229 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn230 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn231 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn232 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn233 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn234 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn171 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn172 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn173 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn174 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn175 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn176 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn177 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn178 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn179 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn180 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn181 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn182 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn183 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn184 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn185 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn186 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn187 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn188 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn189 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn190 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn191 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn192 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn193 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn194 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn147 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn148 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn149 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn150 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn151 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn152 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn153 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn154 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn155 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn156 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn157 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn158 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn159 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn160 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn161 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn162 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn163 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn164 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn165 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn166 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn167 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn168 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn169 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn170 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn123 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn124 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn125 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn126 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn127 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn128 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn129 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn130 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn131 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn132 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn133 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn134 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn135 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn136 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn137 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn138 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn139 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn140 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn141 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn142 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn143 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn144 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn145 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn146 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn99 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn100 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn101 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn102 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn103 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn104 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn105 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn106 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn107 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn108 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn109 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn110 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn111 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn112 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn113 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn114 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn115 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn116 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn117 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn118 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn119 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn120 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn121 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn122 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn75 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn76 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn77 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn78 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn79 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn80 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn81 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn82 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn83 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn84 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn85 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn86 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn87 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn88 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn89 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn90 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn91 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn92 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn93 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn94 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn95 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn96 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn97 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn98 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn51 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn52 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn53 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn54 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn55 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn56 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn57 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn58 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn59 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn60 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn61 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn62 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn63 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn64 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn65 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn66 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn67 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn68 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn69 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn70 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn71 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn72 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn73 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn74 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn33 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn34 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn35 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn36 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn37 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn38 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn39 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn40 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn41 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn42 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn43 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn44 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn45 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn46 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn47 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn48 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.GridColumn357 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn359 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn361 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3781 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPacient, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTePacient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyAuthorization, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyAuthorization.SuspendLayout()
        CType(Me.INDTeStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleAuthorized.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvAuthorized, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleQuoted.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvQuoted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleContracted.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvContracted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleCovered.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCovered, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleFinancedResourceUPC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvFinancedResourceUPC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeItem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeProfessional.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeHealthAdministrator.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeCareGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeCareCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeFolio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeRequestDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeRequestDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeAdmissionNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAdmissionNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCareCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFunctionalUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCareGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciHealthAdministrator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFolio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygAuthorizationInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCovered, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciContracted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciQuoted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAuthorized, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygAditionalInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRequestDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciProfessional, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFinancedResourceUPC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyAuthorization)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1467, 623)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1467, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1467, 98)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygGeneralInformation, Me.INDlygAditionalInformation, Me.INDlygAuthorizationInformation})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1263, 614)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygGeneralInformation
        '
        Me.INDlygGeneralInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneralInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneralInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygGeneralInformation, False)
        Me.INDlygGeneralInformation.CustomizationFormText = "Rubro Presupuestal"
        Me.INDlygGeneralInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciPacient, Me.INDLciAdmissionNumber, Me.INDLciCareCenter, Me.INDLciFunctionalUnit, Me.INDLciCareGroup, Me.INDLciHealthAdministrator, Me.INDLciFolio})
        Me.INDlygGeneralInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygGeneralInformation.Name = "INDlygGeneralInformation"
        Me.INDlygGeneralInformation.Size = New System.Drawing.Size(414, 594)
        Me.INDlygGeneralInformation.Text = "Información Principal"
        '
        'INDLciPacient
        '
        Me.INDLciPacient.Control = Me.INDTePacient
        Me.INDLciPacient.CustomizationFormText = "Nombre"
        Me.INDLciPacient.Location = New System.Drawing.Point(0, 0)
        Me.INDLciPacient.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciPacient.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciPacient.Name = "INDLciPacient"
        Me.INDLciPacient.ShowInCustomizationForm = False
        Me.INDLciPacient.Size = New System.Drawing.Size(390, 64)
        Me.INDLciPacient.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPacient.Text = "Paciente"
        Me.INDLciPacient.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciPacient.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPacient.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciPacient.TextToControlDistance = 5
        '
        'INDTePacient
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTePacient, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTePacient, False)
        Me.INDTePacient.EnterMoveNextControl = True
        Me.INDTePacient.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDTePacient, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTePacient.Name = "INDTePacient"
        Me.INDTePacient.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTePacient.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTePacient.Properties.Appearance.Options.UseBackColor = True
        Me.INDTePacient.Properties.Appearance.Options.UseFont = True
        Me.INDTePacient.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTePacient.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTePacient.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTePacient.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTePacient.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTePacient.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTePacient.Properties.MaxLength = 300
        Me.INDTePacient.Properties.ReadOnly = True
        Me.INDTePacient.Size = New System.Drawing.Size(386, 28)
        Me.INDTePacient.StyleController = Me.INDlyAuthorization
        Me.INDTePacient.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTePacient, 0)
        '
        'INDlyAuthorization
        '
        Me.INDlyAuthorization.AllowCustomization = False
        Me.INDlyAuthorization.Controls.Add(Me.INDTeStatus)
        Me.INDlyAuthorization.Controls.Add(Me.INDGleAuthorized)
        Me.INDlyAuthorization.Controls.Add(Me.INDGleQuoted)
        Me.INDlyAuthorization.Controls.Add(Me.INDGleContracted)
        Me.INDlyAuthorization.Controls.Add(Me.INDGleCovered)
        Me.INDlyAuthorization.Controls.Add(Me.INDGleFinancedResourceUPC)
        Me.INDlyAuthorization.Controls.Add(Me.INDGleType)
        Me.INDlyAuthorization.Controls.Add(Me.INDTeQuantity)
        Me.INDlyAuthorization.Controls.Add(Me.INDTeItem)
        Me.INDlyAuthorization.Controls.Add(Me.INDTeProfessional)
        Me.INDlyAuthorization.Controls.Add(Me.INDTeHealthAdministrator)
        Me.INDlyAuthorization.Controls.Add(Me.INDTeCareGroup)
        Me.INDlyAuthorization.Controls.Add(Me.INDTeFunctionalUnit)
        Me.INDlyAuthorization.Controls.Add(Me.INDTeCareCenter)
        Me.INDlyAuthorization.Controls.Add(Me.INDTeFolio)
        Me.INDlyAuthorization.Controls.Add(Me.INDDeRequestDate)
        Me.INDlyAuthorization.Controls.Add(Me.INDTePacient)
        Me.INDlyAuthorization.Controls.Add(Me.INDTeAdmissionNumber)
        Me.INDlyAuthorization.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyAuthorization, False)
        Me.INDlyAuthorization.Location = New System.Drawing.Point(202, 7)
        Me.INDlyAuthorization.Name = "INDlyAuthorization"
        Me.INDlyAuthorization.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(300, 378, 920, 705)
        Me.INDlyAuthorization.Root = Me.LayoutControlGroup1
        Me.INDlyAuthorization.Size = New System.Drawing.Size(1263, 614)
        Me.INDlyAuthorization.TabIndex = 1
        Me.INDlyAuthorization.Text = "LayoutControl1"
        '
        'INDTeStatus
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeStatus, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeStatus, False)
        Me.INDTeStatus.EnterMoveNextControl = True
        Me.INDTeStatus.Location = New System.Drawing.Point(852, 341)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeStatus, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeStatus.Name = "INDTeStatus"
        Me.INDTeStatus.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTeStatus.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeStatus.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeStatus.Properties.Appearance.Options.UseFont = True
        Me.INDTeStatus.Properties.ReadOnly = True
        Me.INDTeStatus.Size = New System.Drawing.Size(386, 28)
        Me.INDTeStatus.StyleController = Me.INDlyAuthorization
        Me.INDTeStatus.TabIndex = 25
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeStatus, 0)
        '
        'INDGleAuthorized
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleAuthorized, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleAuthorized, False)
        Me.INDGleAuthorized.EnterMoveNextControl = True
        Me.INDGleAuthorized.Location = New System.Drawing.Point(852, 277)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleAuthorized, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleAuthorized.Name = "INDGleAuthorized"
        Me.INDGleAuthorized.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleAuthorized.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleAuthorized.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleAuthorized.Properties.Appearance.Options.UseFont = True
        Me.INDGleAuthorized.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleAuthorized.Properties.DisplayMember = "Item2"
        Me.INDGleAuthorized.Properties.NullText = ""
        Me.INDGleAuthorized.Properties.PopupView = Me.INDGvAuthorized
        Me.INDGleAuthorized.Properties.ReadOnly = True
        Me.INDGleAuthorized.Properties.ValueMember = "Item1"
        Me.INDGleAuthorized.Size = New System.Drawing.Size(386, 28)
        Me.INDGleAuthorized.StyleController = Me.INDlyAuthorization
        Me.INDGleAuthorized.TabIndex = 24
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleAuthorized, 0)
        '
        'INDGvAuthorized
        '
        Me.INDGvAuthorized.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvAuthorized.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvAuthorized.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvAuthorized.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvAuthorized.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAuthorized.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvAuthorized.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAuthorized.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvAuthorized.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvAuthorized.Appearance.Row.Options.UseFont = True
        Me.INDGvAuthorized.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvAuthorized_Description})
        Me.INDGvAuthorized.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvAuthorized.Name = "INDGvAuthorized"
        Me.INDGvAuthorized.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvAuthorized.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvAuthorized.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvAuthorized.OptionsView.ShowAutoFilterRow = True
        Me.INDGvAuthorized.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvAuthorized, False)
        '
        'INDGvAuthorized_Description
        '
        Me.INDGvAuthorized_Description.Caption = "Descripción"
        Me.INDGvAuthorized_Description.FieldName = "Item2"
        Me.INDGvAuthorized_Description.Name = "INDGvAuthorized_Description"
        Me.INDGvAuthorized_Description.Visible = True
        Me.INDGvAuthorized_Description.VisibleIndex = 0
        '
        'INDGleQuoted
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleQuoted, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleQuoted, False)
        Me.INDGleQuoted.EnterMoveNextControl = True
        Me.INDGleQuoted.Location = New System.Drawing.Point(852, 213)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleQuoted, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleQuoted.Name = "INDGleQuoted"
        Me.INDGleQuoted.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleQuoted.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleQuoted.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleQuoted.Properties.Appearance.Options.UseFont = True
        Me.INDGleQuoted.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleQuoted.Properties.DisplayMember = "Item2"
        Me.INDGleQuoted.Properties.NullText = ""
        Me.INDGleQuoted.Properties.PopupView = Me.INDGvQuoted
        Me.INDGleQuoted.Properties.ReadOnly = True
        Me.INDGleQuoted.Properties.ValueMember = "Item1"
        Me.INDGleQuoted.Size = New System.Drawing.Size(386, 28)
        Me.INDGleQuoted.StyleController = Me.INDlyAuthorization
        Me.INDGleQuoted.TabIndex = 23
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleQuoted, 0)
        '
        'INDGvQuoted
        '
        Me.INDGvQuoted.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvQuoted.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvQuoted.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvQuoted.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvQuoted.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvQuoted.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvQuoted.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvQuoted.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvQuoted.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvQuoted.Appearance.Row.Options.UseFont = True
        Me.INDGvQuoted.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvQuoted_Description})
        Me.INDGvQuoted.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvQuoted.Name = "INDGvQuoted"
        Me.INDGvQuoted.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvQuoted.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvQuoted.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvQuoted.OptionsView.ShowAutoFilterRow = True
        Me.INDGvQuoted.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvQuoted, False)
        '
        'INDGvQuoted_Description
        '
        Me.INDGvQuoted_Description.Caption = "Descripción"
        Me.INDGvQuoted_Description.FieldName = "Item2"
        Me.INDGvQuoted_Description.Name = "INDGvQuoted_Description"
        Me.INDGvQuoted_Description.Visible = True
        Me.INDGvQuoted_Description.VisibleIndex = 0
        '
        'INDGleContracted
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleContracted, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleContracted, False)
        Me.INDGleContracted.EnterMoveNextControl = True
        Me.INDGleContracted.Location = New System.Drawing.Point(852, 149)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleContracted, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleContracted.Name = "INDGleContracted"
        Me.INDGleContracted.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleContracted.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleContracted.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleContracted.Properties.Appearance.Options.UseFont = True
        Me.INDGleContracted.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleContracted.Properties.DisplayMember = "Item2"
        Me.INDGleContracted.Properties.NullText = ""
        Me.INDGleContracted.Properties.PopupView = Me.INDGvContracted
        Me.INDGleContracted.Properties.ReadOnly = True
        Me.INDGleContracted.Properties.ValueMember = "Item1"
        Me.INDGleContracted.Size = New System.Drawing.Size(386, 28)
        Me.INDGleContracted.StyleController = Me.INDlyAuthorization
        Me.INDGleContracted.TabIndex = 22
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleContracted, 0)
        '
        'INDGvContracted
        '
        Me.INDGvContracted.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvContracted.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvContracted.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvContracted.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvContracted.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvContracted.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvContracted.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvContracted.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvContracted.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvContracted.Appearance.Row.Options.UseFont = True
        Me.INDGvContracted.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvContracted_Description})
        Me.INDGvContracted.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvContracted.Name = "INDGvContracted"
        Me.INDGvContracted.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvContracted.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvContracted.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvContracted.OptionsView.ShowAutoFilterRow = True
        Me.INDGvContracted.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvContracted, False)
        '
        'INDGvContracted_Description
        '
        Me.INDGvContracted_Description.Caption = "Descripción"
        Me.INDGvContracted_Description.FieldName = "Item2"
        Me.INDGvContracted_Description.Name = "INDGvContracted_Description"
        Me.INDGvContracted_Description.Visible = True
        Me.INDGvContracted_Description.VisibleIndex = 0
        '
        'INDGleCovered
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleCovered, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleCovered, False)
        Me.INDGleCovered.EnterMoveNextControl = True
        Me.INDGleCovered.Location = New System.Drawing.Point(852, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleCovered, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleCovered.Name = "INDGleCovered"
        Me.INDGleCovered.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleCovered.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleCovered.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleCovered.Properties.Appearance.Options.UseFont = True
        Me.INDGleCovered.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleCovered.Properties.DisplayMember = "Item2"
        Me.INDGleCovered.Properties.NullText = ""
        Me.INDGleCovered.Properties.PopupView = Me.INDGvCovered
        Me.INDGleCovered.Properties.ReadOnly = True
        Me.INDGleCovered.Properties.ValueMember = "Item1"
        Me.INDGleCovered.Size = New System.Drawing.Size(386, 28)
        Me.INDGleCovered.StyleController = Me.INDlyAuthorization
        Me.INDGleCovered.TabIndex = 21
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleCovered, 0)
        '
        'INDGvCovered
        '
        Me.INDGvCovered.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvCovered.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvCovered.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvCovered.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvCovered.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCovered.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvCovered.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCovered.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvCovered.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvCovered.Appearance.Row.Options.UseFont = True
        Me.INDGvCovered.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvCovered_Description})
        Me.INDGvCovered.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvCovered.Name = "INDGvCovered"
        Me.INDGvCovered.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvCovered.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCovered.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCovered.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCovered.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvCovered, False)
        '
        'INDGvCovered_Description
        '
        Me.INDGvCovered_Description.Caption = "Descripción"
        Me.INDGvCovered_Description.FieldName = "Item2"
        Me.INDGvCovered_Description.Name = "INDGvCovered_Description"
        Me.INDGvCovered_Description.Visible = True
        Me.INDGvCovered_Description.VisibleIndex = 0
        '
        'INDGleFinancedResourceUPC
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleFinancedResourceUPC, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleFinancedResourceUPC, False)
        Me.INDGleFinancedResourceUPC.EnterMoveNextControl = True
        Me.INDGleFinancedResourceUPC.Location = New System.Drawing.Point(438, 399)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleFinancedResourceUPC, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleFinancedResourceUPC.Name = "INDGleFinancedResourceUPC"
        Me.INDGleFinancedResourceUPC.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleFinancedResourceUPC.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleFinancedResourceUPC.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleFinancedResourceUPC.Properties.Appearance.Options.UseFont = True
        Me.INDGleFinancedResourceUPC.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleFinancedResourceUPC.Properties.DisplayMember = "Item2"
        Me.INDGleFinancedResourceUPC.Properties.NullText = ""
        Me.INDGleFinancedResourceUPC.Properties.PopupView = Me.INDGvFinancedResourceUPC
        Me.INDGleFinancedResourceUPC.Properties.ReadOnly = True
        Me.INDGleFinancedResourceUPC.Properties.ValueMember = "Item1"
        Me.INDGleFinancedResourceUPC.Size = New System.Drawing.Size(386, 28)
        Me.INDGleFinancedResourceUPC.StyleController = Me.INDlyAuthorization
        Me.INDGleFinancedResourceUPC.TabIndex = 20
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleFinancedResourceUPC, 0)
        '
        'INDGvFinancedResourceUPC
        '
        Me.INDGvFinancedResourceUPC.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvFinancedResourceUPC.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvFinancedResourceUPC.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvFinancedResourceUPC.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvFinancedResourceUPC.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvFinancedResourceUPC.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvFinancedResourceUPC.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvFinancedResourceUPC.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvFinancedResourceUPC.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvFinancedResourceUPC.Appearance.Row.Options.UseFont = True
        Me.INDGvFinancedResourceUPC.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvFinancedResourceUPC_Description})
        Me.INDGvFinancedResourceUPC.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvFinancedResourceUPC.Name = "INDGvFinancedResourceUPC"
        Me.INDGvFinancedResourceUPC.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvFinancedResourceUPC.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvFinancedResourceUPC.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvFinancedResourceUPC.OptionsView.ShowAutoFilterRow = True
        Me.INDGvFinancedResourceUPC.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvFinancedResourceUPC, False)
        '
        'INDGvFinancedResourceUPC_Description
        '
        Me.INDGvFinancedResourceUPC_Description.Caption = "Descripción"
        Me.INDGvFinancedResourceUPC_Description.FieldName = "Item2"
        Me.INDGvFinancedResourceUPC_Description.Name = "INDGvFinancedResourceUPC_Description"
        Me.INDGvFinancedResourceUPC_Description.Visible = True
        Me.INDGvFinancedResourceUPC_Description.VisibleIndex = 0
        '
        'INDGleType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleType, False)
        Me.INDGleType.EnterMoveNextControl = True
        Me.INDGleType.Location = New System.Drawing.Point(438, 207)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleType.Name = "INDGleType"
        Me.INDGleType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleType.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleType.Properties.Appearance.Options.UseFont = True
        Me.INDGleType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleType.Properties.DisplayMember = "Item2"
        Me.INDGleType.Properties.NullText = ""
        Me.INDGleType.Properties.PopupView = Me.INDGvType
        Me.INDGleType.Properties.ReadOnly = True
        Me.INDGleType.Properties.ValueMember = "Item1"
        Me.INDGleType.Size = New System.Drawing.Size(386, 28)
        Me.INDGleType.StyleController = Me.INDlyAuthorization
        Me.INDGleType.TabIndex = 19
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleType, 0)
        '
        'INDGvType
        '
        Me.INDGvType.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvType.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvType.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvType.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvType.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvType.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvType.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvType.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvType.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvType.Appearance.Row.Options.UseFont = True
        Me.INDGvType.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvType_Description})
        Me.INDGvType.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvType.Name = "INDGvType"
        Me.INDGvType.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvType.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvType.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvType.OptionsView.ShowAutoFilterRow = True
        Me.INDGvType.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvType, False)
        '
        'INDGvType_Description
        '
        Me.INDGvType_Description.Caption = "Descripción"
        Me.INDGvType_Description.FieldName = "Item2"
        Me.INDGvType_Description.Name = "INDGvType_Description"
        Me.INDGvType_Description.Visible = True
        Me.INDGvType_Description.VisibleIndex = 0
        '
        'INDTeQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeQuantity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeQuantity, False)
        Me.INDTeQuantity.EditValue = ""
        Me.INDTeQuantity.EnterMoveNextControl = True
        Me.INDTeQuantity.Location = New System.Drawing.Point(438, 341)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeQuantity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeQuantity.Name = "INDTeQuantity"
        Me.INDTeQuantity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTeQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDTeQuantity.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTeQuantity.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTeQuantity.Properties.Mask.EditMask = "n0"
        Me.INDTeQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTeQuantity.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTeQuantity.Properties.ReadOnly = True
        Me.INDTeQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDTeQuantity.StyleController = Me.INDlyAuthorization
        Me.INDTeQuantity.TabIndex = 17
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeQuantity, 0)
        '
        'INDTeItem
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeItem, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeItem, False)
        Me.INDTeItem.EnterMoveNextControl = True
        Me.INDTeItem.Location = New System.Drawing.Point(438, 277)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeItem, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeItem.Name = "INDTeItem"
        Me.INDTeItem.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTeItem.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeItem.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeItem.Properties.Appearance.Options.UseFont = True
        Me.INDTeItem.Properties.ReadOnly = True
        Me.INDTeItem.Size = New System.Drawing.Size(386, 28)
        Me.INDTeItem.StyleController = Me.INDlyAuthorization
        Me.INDTeItem.TabIndex = 16
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeItem, 0)
        '
        'INDTeProfessional
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeProfessional, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeProfessional, False)
        Me.INDTeProfessional.EnterMoveNextControl = True
        Me.INDTeProfessional.Location = New System.Drawing.Point(438, 149)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeProfessional, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeProfessional.Name = "INDTeProfessional"
        Me.INDTeProfessional.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTeProfessional.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeProfessional.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeProfessional.Properties.Appearance.Options.UseFont = True
        Me.INDTeProfessional.Properties.ReadOnly = True
        Me.INDTeProfessional.Size = New System.Drawing.Size(386, 28)
        Me.INDTeProfessional.StyleController = Me.INDlyAuthorization
        Me.INDTeProfessional.TabIndex = 14
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeProfessional, 0)
        '
        'INDTeHealthAdministrator
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeHealthAdministrator, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeHealthAdministrator, False)
        Me.INDTeHealthAdministrator.EnterMoveNextControl = True
        Me.INDTeHealthAdministrator.Location = New System.Drawing.Point(24, 469)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeHealthAdministrator, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeHealthAdministrator.Name = "INDTeHealthAdministrator"
        Me.INDTeHealthAdministrator.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTeHealthAdministrator.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeHealthAdministrator.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeHealthAdministrator.Properties.Appearance.Options.UseFont = True
        Me.INDTeHealthAdministrator.Properties.ReadOnly = True
        Me.INDTeHealthAdministrator.Size = New System.Drawing.Size(386, 28)
        Me.INDTeHealthAdministrator.StyleController = Me.INDlyAuthorization
        Me.INDTeHealthAdministrator.TabIndex = 13
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeHealthAdministrator, 0)
        '
        'INDTeCareGroup
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeCareGroup, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeCareGroup, False)
        Me.INDTeCareGroup.EnterMoveNextControl = True
        Me.INDTeCareGroup.Location = New System.Drawing.Point(24, 405)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeCareGroup, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeCareGroup.Name = "INDTeCareGroup"
        Me.INDTeCareGroup.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTeCareGroup.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeCareGroup.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeCareGroup.Properties.Appearance.Options.UseFont = True
        Me.INDTeCareGroup.Properties.ReadOnly = True
        Me.INDTeCareGroup.Size = New System.Drawing.Size(386, 28)
        Me.INDTeCareGroup.StyleController = Me.INDlyAuthorization
        Me.INDTeCareGroup.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeCareGroup, 0)
        '
        'INDTeFunctionalUnit
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeFunctionalUnit, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeFunctionalUnit, False)
        Me.INDTeFunctionalUnit.EnterMoveNextControl = True
        Me.INDTeFunctionalUnit.Location = New System.Drawing.Point(24, 341)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeFunctionalUnit, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeFunctionalUnit.Name = "INDTeFunctionalUnit"
        Me.INDTeFunctionalUnit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTeFunctionalUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeFunctionalUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeFunctionalUnit.Properties.Appearance.Options.UseFont = True
        Me.INDTeFunctionalUnit.Properties.ReadOnly = True
        Me.INDTeFunctionalUnit.Size = New System.Drawing.Size(386, 28)
        Me.INDTeFunctionalUnit.StyleController = Me.INDlyAuthorization
        Me.INDTeFunctionalUnit.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeFunctionalUnit, 0)
        '
        'INDTeCareCenter
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeCareCenter, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeCareCenter, False)
        Me.INDTeCareCenter.EnterMoveNextControl = True
        Me.INDTeCareCenter.Location = New System.Drawing.Point(24, 277)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeCareCenter, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeCareCenter.Name = "INDTeCareCenter"
        Me.INDTeCareCenter.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTeCareCenter.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeCareCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeCareCenter.Properties.Appearance.Options.UseFont = True
        Me.INDTeCareCenter.Properties.ReadOnly = True
        Me.INDTeCareCenter.Size = New System.Drawing.Size(386, 28)
        Me.INDTeCareCenter.StyleController = Me.INDlyAuthorization
        Me.INDTeCareCenter.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeCareCenter, 0)
        '
        'INDTeFolio
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeFolio, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeFolio, False)
        Me.INDTeFolio.EnterMoveNextControl = True
        Me.INDTeFolio.Location = New System.Drawing.Point(24, 213)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeFolio, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeFolio.Name = "INDTeFolio"
        Me.INDTeFolio.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTeFolio.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeFolio.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeFolio.Properties.Appearance.Options.UseFont = True
        Me.INDTeFolio.Properties.ReadOnly = True
        Me.INDTeFolio.Size = New System.Drawing.Size(386, 28)
        Me.INDTeFolio.StyleController = Me.INDlyAuthorization
        Me.INDTeFolio.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeFolio, 0)
        '
        'INDDeRequestDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeRequestDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeRequestDate, False)
        Me.INDDeRequestDate.EditValue = Nothing
        Me.INDDeRequestDate.EnterMoveNextControl = True
        Me.INDDeRequestDate.Location = New System.Drawing.Point(438, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeRequestDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDDeRequestDate.Name = "INDDeRequestDate"
        Me.INDDeRequestDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDDeRequestDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeRequestDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeRequestDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeRequestDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeRequestDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeRequestDate.Properties.Mask.EditMask = "F"
        Me.INDDeRequestDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeRequestDate.Properties.ReadOnly = True
        Me.INDDeRequestDate.Size = New System.Drawing.Size(386, 28)
        Me.INDDeRequestDate.StyleController = Me.INDlyAuthorization
        Me.INDDeRequestDate.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeRequestDate, 0)
        '
        'INDTeAdmissionNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeAdmissionNumber, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeAdmissionNumber, False)
        Me.INDTeAdmissionNumber.EnterMoveNextControl = True
        Me.INDTeAdmissionNumber.Location = New System.Drawing.Point(24, 149)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeAdmissionNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeAdmissionNumber.Name = "INDTeAdmissionNumber"
        Me.INDTeAdmissionNumber.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTeAdmissionNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeAdmissionNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDTeAdmissionNumber.Properties.Appearance.Options.UseFont = True
        Me.INDTeAdmissionNumber.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTeAdmissionNumber.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTeAdmissionNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeAdmissionNumber.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTeAdmissionNumber.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTeAdmissionNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTeAdmissionNumber.Properties.MaxLength = 20
        Me.INDTeAdmissionNumber.Properties.ReadOnly = True
        Me.INDTeAdmissionNumber.Size = New System.Drawing.Size(386, 28)
        Me.INDTeAdmissionNumber.StyleController = Me.INDlyAuthorization
        Me.INDTeAdmissionNumber.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeAdmissionNumber, 0)
        '
        'INDLciAdmissionNumber
        '
        Me.INDLciAdmissionNumber.Control = Me.INDTeAdmissionNumber
        Me.INDLciAdmissionNumber.CustomizationFormText = "Código Alterno"
        Me.INDLciAdmissionNumber.Location = New System.Drawing.Point(0, 64)
        Me.INDLciAdmissionNumber.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciAdmissionNumber.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciAdmissionNumber.Name = "INDLciAdmissionNumber"
        Me.INDLciAdmissionNumber.Size = New System.Drawing.Size(390, 64)
        Me.INDLciAdmissionNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAdmissionNumber.Text = "Ingreso"
        Me.INDLciAdmissionNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciAdmissionNumber.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciAdmissionNumber.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciAdmissionNumber.TextToControlDistance = 5
        '
        'INDLciCareCenter
        '
        Me.INDLciCareCenter.Control = Me.INDTeCareCenter
        Me.INDLciCareCenter.Location = New System.Drawing.Point(0, 192)
        Me.INDLciCareCenter.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciCareCenter.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciCareCenter.Name = "INDLciCareCenter"
        Me.INDLciCareCenter.Size = New System.Drawing.Size(390, 64)
        Me.INDLciCareCenter.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCareCenter.Text = "Centro de Atención"
        Me.INDLciCareCenter.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCareCenter.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCareCenter.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciCareCenter.TextToControlDistance = 5
        '
        'INDLciFunctionalUnit
        '
        Me.INDLciFunctionalUnit.Control = Me.INDTeFunctionalUnit
        Me.INDLciFunctionalUnit.Location = New System.Drawing.Point(0, 256)
        Me.INDLciFunctionalUnit.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciFunctionalUnit.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciFunctionalUnit.Name = "INDLciFunctionalUnit"
        Me.INDLciFunctionalUnit.Size = New System.Drawing.Size(390, 64)
        Me.INDLciFunctionalUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFunctionalUnit.Text = "Unidad Funcional"
        Me.INDLciFunctionalUnit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciFunctionalUnit.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciFunctionalUnit.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciFunctionalUnit.TextToControlDistance = 5
        '
        'INDLciCareGroup
        '
        Me.INDLciCareGroup.Control = Me.INDTeCareGroup
        Me.INDLciCareGroup.Location = New System.Drawing.Point(0, 320)
        Me.INDLciCareGroup.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciCareGroup.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciCareGroup.Name = "INDLciCareGroup"
        Me.INDLciCareGroup.Size = New System.Drawing.Size(390, 64)
        Me.INDLciCareGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCareGroup.Text = "Grupo Atención"
        Me.INDLciCareGroup.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCareGroup.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCareGroup.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciCareGroup.TextToControlDistance = 5
        '
        'INDLciHealthAdministrator
        '
        Me.INDLciHealthAdministrator.Control = Me.INDTeHealthAdministrator
        Me.INDLciHealthAdministrator.Location = New System.Drawing.Point(0, 384)
        Me.INDLciHealthAdministrator.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciHealthAdministrator.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciHealthAdministrator.Name = "INDLciHealthAdministrator"
        Me.INDLciHealthAdministrator.Size = New System.Drawing.Size(390, 151)
        Me.INDLciHealthAdministrator.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciHealthAdministrator.Text = "Entidad Responsable"
        Me.INDLciHealthAdministrator.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciHealthAdministrator.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciHealthAdministrator.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciHealthAdministrator.TextToControlDistance = 5
        '
        'INDLciFolio
        '
        Me.INDLciFolio.Control = Me.INDTeFolio
        Me.INDLciFolio.Location = New System.Drawing.Point(0, 128)
        Me.INDLciFolio.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciFolio.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciFolio.Name = "INDLciFolio"
        Me.INDLciFolio.Size = New System.Drawing.Size(390, 64)
        Me.INDLciFolio.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFolio.Text = "Folio"
        Me.INDLciFolio.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciFolio.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciFolio.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciFolio.TextToControlDistance = 5
        '
        'INDlygAuthorizationInformation
        '
        Me.INDlygAuthorizationInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygAuthorizationInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlygAuthorizationInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygAuthorizationInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygAuthorizationInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAuthorizationInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygAuthorizationInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygAuthorizationInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygAuthorizationInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAuthorizationInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygAuthorizationInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAuthorizationInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygAuthorizationInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAuthorizationInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygAuthorizationInformation, False)
        Me.INDlygAuthorizationInformation.CustomizationFormText = "Información Principal"
        Me.INDlygAuthorizationInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCovered, Me.INDLciContracted, Me.INDLciQuoted, Me.INDLciAuthorized, Me.INDLciStatus})
        Me.INDlygAuthorizationInformation.Location = New System.Drawing.Point(828, 0)
        Me.INDlygAuthorizationInformation.Name = "INDlygAuthorizationInformation"
        Me.INDlygAuthorizationInformation.Size = New System.Drawing.Size(415, 594)
        Me.INDlygAuthorizationInformation.Text = "Información Autorización"
        '
        'INDLciCovered
        '
        Me.INDLciCovered.Control = Me.INDGleCovered
        Me.INDLciCovered.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCovered.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciCovered.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciCovered.Name = "INDLciCovered"
        Me.INDLciCovered.Size = New System.Drawing.Size(391, 64)
        Me.INDLciCovered.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCovered.Text = "Cubierto"
        Me.INDLciCovered.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCovered.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCovered.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciCovered.TextToControlDistance = 5
        '
        'INDLciContracted
        '
        Me.INDLciContracted.Control = Me.INDGleContracted
        Me.INDLciContracted.Location = New System.Drawing.Point(0, 64)
        Me.INDLciContracted.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciContracted.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciContracted.Name = "INDLciContracted"
        Me.INDLciContracted.Size = New System.Drawing.Size(391, 64)
        Me.INDLciContracted.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciContracted.Text = "Contratado"
        Me.INDLciContracted.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciContracted.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciContracted.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciContracted.TextToControlDistance = 5
        '
        'INDLciQuoted
        '
        Me.INDLciQuoted.Control = Me.INDGleQuoted
        Me.INDLciQuoted.Location = New System.Drawing.Point(0, 128)
        Me.INDLciQuoted.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciQuoted.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciQuoted.Name = "INDLciQuoted"
        Me.INDLciQuoted.Size = New System.Drawing.Size(391, 64)
        Me.INDLciQuoted.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciQuoted.Text = "Cotizado"
        Me.INDLciQuoted.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciQuoted.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciQuoted.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciQuoted.TextToControlDistance = 5
        '
        'INDLciAuthorized
        '
        Me.INDLciAuthorized.Control = Me.INDGleAuthorized
        Me.INDLciAuthorized.Location = New System.Drawing.Point(0, 192)
        Me.INDLciAuthorized.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciAuthorized.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciAuthorized.Name = "INDLciAuthorized"
        Me.INDLciAuthorized.Size = New System.Drawing.Size(391, 64)
        Me.INDLciAuthorized.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAuthorized.Text = "Requiere Autorización"
        Me.INDLciAuthorized.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciAuthorized.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciAuthorized.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciAuthorized.TextToControlDistance = 5
        '
        'INDLciStatus
        '
        Me.INDLciStatus.Control = Me.INDTeStatus
        Me.INDLciStatus.Location = New System.Drawing.Point(0, 256)
        Me.INDLciStatus.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciStatus.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciStatus.Name = "INDLciStatus"
        Me.INDLciStatus.Size = New System.Drawing.Size(391, 279)
        Me.INDLciStatus.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciStatus.Text = "Estado"
        Me.INDLciStatus.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciStatus.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciStatus.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciStatus.TextToControlDistance = 5
        '
        'INDlygAditionalInformation
        '
        Me.INDlygAditionalInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygAditionalInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlygAditionalInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygAditionalInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygAditionalInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAditionalInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygAditionalInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygAditionalInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygAditionalInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAditionalInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygAditionalInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAditionalInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygAditionalInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygAditionalInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygAditionalInformation, False)
        Me.INDlygAditionalInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciRequestDate, Me.INDLciProfessional, Me.INDLciItem, Me.INDLciQuantity, Me.INDLciType, Me.INDLciFinancedResourceUPC})
        Me.INDlygAditionalInformation.Location = New System.Drawing.Point(414, 0)
        Me.INDlygAditionalInformation.Name = "INDlygAditionalInformation"
        Me.INDlygAditionalInformation.Size = New System.Drawing.Size(414, 594)
        Me.INDlygAditionalInformation.Text = "Información Adicional"
        '
        'INDLciRequestDate
        '
        Me.INDLciRequestDate.Control = Me.INDDeRequestDate
        Me.INDLciRequestDate.Location = New System.Drawing.Point(0, 0)
        Me.INDLciRequestDate.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciRequestDate.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciRequestDate.Name = "INDLciRequestDate"
        Me.INDLciRequestDate.Size = New System.Drawing.Size(390, 64)
        Me.INDLciRequestDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciRequestDate.Text = "Fecha Solicitud"
        Me.INDLciRequestDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciRequestDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciRequestDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciRequestDate.TextToControlDistance = 5
        '
        'INDLciProfessional
        '
        Me.INDLciProfessional.Control = Me.INDTeProfessional
        Me.INDLciProfessional.Location = New System.Drawing.Point(0, 64)
        Me.INDLciProfessional.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciProfessional.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciProfessional.Name = "INDLciProfessional"
        Me.INDLciProfessional.Size = New System.Drawing.Size(390, 64)
        Me.INDLciProfessional.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciProfessional.Text = "Medico Solicitante"
        Me.INDLciProfessional.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciProfessional.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciProfessional.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciProfessional.TextToControlDistance = 5
        '
        'INDLciItem
        '
        Me.INDLciItem.Control = Me.INDTeItem
        Me.INDLciItem.Location = New System.Drawing.Point(0, 192)
        Me.INDLciItem.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciItem.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciItem.Name = "INDLciItem"
        Me.INDLciItem.Size = New System.Drawing.Size(390, 64)
        Me.INDLciItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciItem.Text = "Servicio"
        Me.INDLciItem.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciItem.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciItem.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciItem.TextToControlDistance = 5
        '
        'INDLciQuantity
        '
        Me.INDLciQuantity.Control = Me.INDTeQuantity
        Me.INDLciQuantity.Location = New System.Drawing.Point(0, 256)
        Me.INDLciQuantity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciQuantity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciQuantity.Name = "INDLciQuantity"
        Me.INDLciQuantity.Size = New System.Drawing.Size(390, 64)
        Me.INDLciQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciQuantity.Text = "Cantidad"
        Me.INDLciQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciQuantity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciQuantity.TextToControlDistance = 5
        '
        'INDLciType
        '
        Me.INDLciType.Control = Me.INDGleType
        Me.INDLciType.Location = New System.Drawing.Point(0, 128)
        Me.INDLciType.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciType.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciType.Name = "INDLciType"
        Me.INDLciType.Size = New System.Drawing.Size(390, 64)
        Me.INDLciType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciType.Text = "Tipo"
        Me.INDLciType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciType.TextSize = New System.Drawing.Size(222, 17)
        '
        'INDLciFinancedResourceUPC
        '
        Me.INDLciFinancedResourceUPC.Control = Me.INDGleFinancedResourceUPC
        Me.INDLciFinancedResourceUPC.Location = New System.Drawing.Point(0, 320)
        Me.INDLciFinancedResourceUPC.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciFinancedResourceUPC.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciFinancedResourceUPC.Name = "INDLciFinancedResourceUPC"
        Me.INDLciFinancedResourceUPC.Size = New System.Drawing.Size(390, 215)
        Me.INDLciFinancedResourceUPC.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFinancedResourceUPC.Text = "Financiado con recursos de la UPC"
        Me.INDLciFinancedResourceUPC.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciFinancedResourceUPC.TextSize = New System.Drawing.Size(222, 17)
        '
        'GridColumn375
        '
        Me.GridColumn375.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn375.Caption = "Selección"
        Me.GridColumn375.FieldName = "Item2"
        Me.GridColumn375.Name = "GridColumn375"
        '
        'GridColumn376
        '
        Me.GridColumn376.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn376.Caption = "Selección"
        Me.GridColumn376.FieldName = "Item2"
        Me.GridColumn376.Name = "GridColumn376"
        '
        'GridColumn377
        '
        Me.GridColumn377.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn377.Caption = "Selección"
        Me.GridColumn377.FieldName = "Item2"
        Me.GridColumn377.Name = "GridColumn377"
        '
        'GridColumn372
        '
        Me.GridColumn372.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn372.Caption = "Selección"
        Me.GridColumn372.FieldName = "Item2"
        Me.GridColumn372.Name = "GridColumn372"
        '
        'GridColumn373
        '
        Me.GridColumn373.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn373.Caption = "Selección"
        Me.GridColumn373.FieldName = "Item2"
        Me.GridColumn373.Name = "GridColumn373"
        '
        'GridColumn374
        '
        Me.GridColumn374.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn374.Caption = "Selección"
        Me.GridColumn374.FieldName = "Item2"
        Me.GridColumn374.Name = "GridColumn374"
        '
        'GridColumn369
        '
        Me.GridColumn369.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn369.Caption = "Selección"
        Me.GridColumn369.FieldName = "Item2"
        Me.GridColumn369.Name = "GridColumn369"
        '
        'GridColumn370
        '
        Me.GridColumn370.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn370.Caption = "Selección"
        Me.GridColumn370.FieldName = "Item2"
        Me.GridColumn370.Name = "GridColumn370"
        '
        'GridColumn371
        '
        Me.GridColumn371.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn371.Caption = "Selección"
        Me.GridColumn371.FieldName = "Item2"
        Me.GridColumn371.Name = "GridColumn371"
        '
        'GridColumn366
        '
        Me.GridColumn366.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn366.Caption = "Selección"
        Me.GridColumn366.FieldName = "Item2"
        Me.GridColumn366.Name = "GridColumn366"
        '
        'GridColumn367
        '
        Me.GridColumn367.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn367.Caption = "Selección"
        Me.GridColumn367.FieldName = "Item2"
        Me.GridColumn367.Name = "GridColumn367"
        '
        'GridColumn368
        '
        Me.GridColumn368.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn368.Caption = "Selección"
        Me.GridColumn368.FieldName = "Item2"
        Me.GridColumn368.Name = "GridColumn368"
        '
        'GridColumn363
        '
        Me.GridColumn363.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn363.Caption = "Selección"
        Me.GridColumn363.FieldName = "Item2"
        Me.GridColumn363.Name = "GridColumn363"
        '
        'GridColumn364
        '
        Me.GridColumn364.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn364.Caption = "Selección"
        Me.GridColumn364.FieldName = "Item2"
        Me.GridColumn364.Name = "GridColumn364"
        '
        'GridColumn365
        '
        Me.GridColumn365.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn365.Caption = "Selección"
        Me.GridColumn365.FieldName = "Item2"
        Me.GridColumn365.Name = "GridColumn365"
        '
        'GridColumn362
        '
        Me.GridColumn362.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn362.Caption = "Selección"
        Me.GridColumn362.FieldName = "Item2"
        Me.GridColumn362.Name = "GridColumn362"
        '
        'GridColumn360
        '
        Me.GridColumn360.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn360.Caption = "Selección"
        Me.GridColumn360.FieldName = "Item2"
        Me.GridColumn360.Name = "GridColumn360"
        '
        'GridColumn358
        '
        Me.GridColumn358.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn358.Caption = "Selección"
        Me.GridColumn358.FieldName = "Item2"
        Me.GridColumn358.Name = "GridColumn358"
        '
        'GridColumn315
        '
        Me.GridColumn315.Caption = "Id"
        Me.GridColumn315.FieldName = "Id"
        Me.GridColumn315.Name = "GridColumn315"
        '
        'GridColumn316
        '
        Me.GridColumn316.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn316.Caption = "Código"
        Me.GridColumn316.FieldName = "Number"
        Me.GridColumn316.Name = "GridColumn316"
        Me.GridColumn316.Width = 300
        '
        'GridColumn317
        '
        Me.GridColumn317.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn317.Caption = "Nombre"
        Me.GridColumn317.FieldName = "Name"
        Me.GridColumn317.Name = "GridColumn317"
        Me.GridColumn317.Width = 350
        '
        'GridColumn318
        '
        Me.GridColumn318.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn318.Caption = "Maneja Tercero"
        Me.GridColumn318.FieldName = "HandlesThirdParty"
        Me.GridColumn318.Name = "GridColumn318"
        Me.GridColumn318.Width = 350
        '
        'GridColumn319
        '
        Me.GridColumn319.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn319.Caption = "Maneja Centro de Costo"
        Me.GridColumn319.FieldName = "HandlesCostCenter"
        Me.GridColumn319.Name = "GridColumn319"
        Me.GridColumn319.Width = 350
        '
        'GridColumn320
        '
        Me.GridColumn320.Caption = "Id"
        Me.GridColumn320.FieldName = "Id"
        Me.GridColumn320.Name = "GridColumn320"
        '
        'GridColumn321
        '
        Me.GridColumn321.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn321.Caption = "Código"
        Me.GridColumn321.FieldName = "Number"
        Me.GridColumn321.Name = "GridColumn321"
        Me.GridColumn321.Width = 300
        '
        'GridColumn322
        '
        Me.GridColumn322.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn322.Caption = "Nombre"
        Me.GridColumn322.FieldName = "Name"
        Me.GridColumn322.Name = "GridColumn322"
        Me.GridColumn322.Width = 350
        '
        'GridColumn323
        '
        Me.GridColumn323.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn323.Caption = "Maneja Tercero"
        Me.GridColumn323.FieldName = "HandlesThirdParty"
        Me.GridColumn323.Name = "GridColumn323"
        Me.GridColumn323.Width = 350
        '
        'GridColumn324
        '
        Me.GridColumn324.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn324.Caption = "Maneja Centro de Costo"
        Me.GridColumn324.FieldName = "HandlesCostCenter"
        Me.GridColumn324.Name = "GridColumn324"
        Me.GridColumn324.Width = 350
        '
        'GridColumn325
        '
        Me.GridColumn325.Caption = "Id"
        Me.GridColumn325.FieldName = "Id"
        Me.GridColumn325.Name = "GridColumn325"
        '
        'GridColumn326
        '
        Me.GridColumn326.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn326.Caption = "Código"
        Me.GridColumn326.FieldName = "Number"
        Me.GridColumn326.Name = "GridColumn326"
        Me.GridColumn326.Width = 300
        '
        'GridColumn327
        '
        Me.GridColumn327.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn327.Caption = "Nombre"
        Me.GridColumn327.FieldName = "Name"
        Me.GridColumn327.Name = "GridColumn327"
        Me.GridColumn327.Width = 350
        '
        'GridColumn328
        '
        Me.GridColumn328.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn328.Caption = "Maneja Tercero"
        Me.GridColumn328.FieldName = "HandlesThirdParty"
        Me.GridColumn328.Name = "GridColumn328"
        Me.GridColumn328.Width = 350
        '
        'GridColumn329
        '
        Me.GridColumn329.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn329.Caption = "Maneja Centro de Costo"
        Me.GridColumn329.FieldName = "HandlesCostCenter"
        Me.GridColumn329.Name = "GridColumn329"
        Me.GridColumn329.Width = 350
        '
        'GridColumn330
        '
        Me.GridColumn330.Caption = "Id"
        Me.GridColumn330.FieldName = "Id"
        Me.GridColumn330.Name = "GridColumn330"
        '
        'GridColumn331
        '
        Me.GridColumn331.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn331.Caption = "Código"
        Me.GridColumn331.FieldName = "Number"
        Me.GridColumn331.Name = "GridColumn331"
        Me.GridColumn331.Width = 300
        '
        'GridColumn332
        '
        Me.GridColumn332.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn332.Caption = "Nombre"
        Me.GridColumn332.FieldName = "Name"
        Me.GridColumn332.Name = "GridColumn332"
        Me.GridColumn332.Width = 350
        '
        'GridColumn333
        '
        Me.GridColumn333.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn333.Caption = "Maneja Tercero"
        Me.GridColumn333.FieldName = "HandlesThirdParty"
        Me.GridColumn333.Name = "GridColumn333"
        Me.GridColumn333.Width = 350
        '
        'GridColumn334
        '
        Me.GridColumn334.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn334.Caption = "Maneja Centro de Costo"
        Me.GridColumn334.FieldName = "HandlesCostCenter"
        Me.GridColumn334.Name = "GridColumn334"
        Me.GridColumn334.Width = 350
        '
        'GridColumn335
        '
        Me.GridColumn335.Caption = "Id"
        Me.GridColumn335.FieldName = "Id"
        Me.GridColumn335.Name = "GridColumn335"
        '
        'GridColumn336
        '
        Me.GridColumn336.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn336.Caption = "Código"
        Me.GridColumn336.FieldName = "Number"
        Me.GridColumn336.Name = "GridColumn336"
        Me.GridColumn336.Width = 300
        '
        'GridColumn337
        '
        Me.GridColumn337.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn337.Caption = "Nombre"
        Me.GridColumn337.FieldName = "Name"
        Me.GridColumn337.Name = "GridColumn337"
        Me.GridColumn337.Width = 350
        '
        'GridColumn338
        '
        Me.GridColumn338.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn338.Caption = "Maneja Tercero"
        Me.GridColumn338.FieldName = "HandlesThirdParty"
        Me.GridColumn338.Name = "GridColumn338"
        Me.GridColumn338.Width = 350
        '
        'GridColumn339
        '
        Me.GridColumn339.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn339.Caption = "Maneja Centro de Costo"
        Me.GridColumn339.FieldName = "HandlesCostCenter"
        Me.GridColumn339.Name = "GridColumn339"
        Me.GridColumn339.Width = 350
        '
        'GridColumn340
        '
        Me.GridColumn340.Caption = "Id"
        Me.GridColumn340.FieldName = "Id"
        Me.GridColumn340.Name = "GridColumn340"
        '
        'GridColumn341
        '
        Me.GridColumn341.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn341.Caption = "Código"
        Me.GridColumn341.FieldName = "Number"
        Me.GridColumn341.Name = "GridColumn341"
        Me.GridColumn341.Width = 300
        '
        'GridColumn342
        '
        Me.GridColumn342.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn342.Caption = "Nombre"
        Me.GridColumn342.FieldName = "Name"
        Me.GridColumn342.Name = "GridColumn342"
        Me.GridColumn342.Width = 350
        '
        'GridColumn343
        '
        Me.GridColumn343.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn343.Caption = "Maneja Tercero"
        Me.GridColumn343.FieldName = "HandlesThirdParty"
        Me.GridColumn343.Name = "GridColumn343"
        Me.GridColumn343.Width = 350
        '
        'GridColumn344
        '
        Me.GridColumn344.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn344.Caption = "Maneja Centro de Costo"
        Me.GridColumn344.FieldName = "HandlesCostCenter"
        Me.GridColumn344.Name = "GridColumn344"
        Me.GridColumn344.Width = 350
        '
        'GridColumn345
        '
        Me.GridColumn345.Caption = "Id"
        Me.GridColumn345.FieldName = "Id"
        Me.GridColumn345.Name = "GridColumn345"
        '
        'GridColumn346
        '
        Me.GridColumn346.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn346.Caption = "Código"
        Me.GridColumn346.FieldName = "Number"
        Me.GridColumn346.Name = "GridColumn346"
        Me.GridColumn346.Width = 300
        '
        'GridColumn347
        '
        Me.GridColumn347.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn347.Caption = "Nombre"
        Me.GridColumn347.FieldName = "Name"
        Me.GridColumn347.Name = "GridColumn347"
        Me.GridColumn347.Width = 350
        '
        'GridColumn348
        '
        Me.GridColumn348.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn348.Caption = "Maneja Tercero"
        Me.GridColumn348.FieldName = "HandlesThirdParty"
        Me.GridColumn348.Name = "GridColumn348"
        Me.GridColumn348.Width = 350
        '
        'GridColumn349
        '
        Me.GridColumn349.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn349.Caption = "Maneja Centro de Costo"
        Me.GridColumn349.FieldName = "HandlesCostCenter"
        Me.GridColumn349.Name = "GridColumn349"
        Me.GridColumn349.Width = 350
        '
        'GridColumn350
        '
        Me.GridColumn350.Caption = "Id"
        Me.GridColumn350.FieldName = "Id"
        Me.GridColumn350.Name = "GridColumn350"
        '
        'GridColumn351
        '
        Me.GridColumn351.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn351.Caption = "Código"
        Me.GridColumn351.FieldName = "Number"
        Me.GridColumn351.Name = "GridColumn351"
        Me.GridColumn351.Width = 300
        '
        'GridColumn352
        '
        Me.GridColumn352.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn352.Caption = "Nombre"
        Me.GridColumn352.FieldName = "Name"
        Me.GridColumn352.Name = "GridColumn352"
        Me.GridColumn352.Width = 350
        '
        'GridColumn353
        '
        Me.GridColumn353.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn353.Caption = "Maneja Tercero"
        Me.GridColumn353.FieldName = "HandlesThirdParty"
        Me.GridColumn353.Name = "GridColumn353"
        Me.GridColumn353.Width = 350
        '
        'GridColumn354
        '
        Me.GridColumn354.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn354.Caption = "Maneja Centro de Costo"
        Me.GridColumn354.FieldName = "HandlesCostCenter"
        Me.GridColumn354.Name = "GridColumn354"
        Me.GridColumn354.Width = 350
        '
        'GridColumn275
        '
        Me.GridColumn275.Caption = "Id"
        Me.GridColumn275.FieldName = "Id"
        Me.GridColumn275.Name = "GridColumn275"
        '
        'GridColumn276
        '
        Me.GridColumn276.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn276.Caption = "Código"
        Me.GridColumn276.FieldName = "Number"
        Me.GridColumn276.Name = "GridColumn276"
        Me.GridColumn276.Width = 300
        '
        'GridColumn277
        '
        Me.GridColumn277.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn277.Caption = "Nombre"
        Me.GridColumn277.FieldName = "Name"
        Me.GridColumn277.Name = "GridColumn277"
        Me.GridColumn277.Width = 350
        '
        'GridColumn278
        '
        Me.GridColumn278.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn278.Caption = "Maneja Tercero"
        Me.GridColumn278.FieldName = "HandlesThirdParty"
        Me.GridColumn278.Name = "GridColumn278"
        Me.GridColumn278.Width = 350
        '
        'GridColumn279
        '
        Me.GridColumn279.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn279.Caption = "Maneja Centro de Costo"
        Me.GridColumn279.FieldName = "HandlesCostCenter"
        Me.GridColumn279.Name = "GridColumn279"
        Me.GridColumn279.Width = 350
        '
        'GridColumn280
        '
        Me.GridColumn280.Caption = "Id"
        Me.GridColumn280.FieldName = "Id"
        Me.GridColumn280.Name = "GridColumn280"
        '
        'GridColumn281
        '
        Me.GridColumn281.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn281.Caption = "Código"
        Me.GridColumn281.FieldName = "Number"
        Me.GridColumn281.Name = "GridColumn281"
        Me.GridColumn281.Width = 300
        '
        'GridColumn282
        '
        Me.GridColumn282.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn282.Caption = "Nombre"
        Me.GridColumn282.FieldName = "Name"
        Me.GridColumn282.Name = "GridColumn282"
        Me.GridColumn282.Width = 350
        '
        'GridColumn283
        '
        Me.GridColumn283.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn283.Caption = "Maneja Tercero"
        Me.GridColumn283.FieldName = "HandlesThirdParty"
        Me.GridColumn283.Name = "GridColumn283"
        Me.GridColumn283.Width = 350
        '
        'GridColumn284
        '
        Me.GridColumn284.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn284.Caption = "Maneja Centro de Costo"
        Me.GridColumn284.FieldName = "HandlesCostCenter"
        Me.GridColumn284.Name = "GridColumn284"
        Me.GridColumn284.Width = 350
        '
        'GridColumn285
        '
        Me.GridColumn285.Caption = "Id"
        Me.GridColumn285.FieldName = "Id"
        Me.GridColumn285.Name = "GridColumn285"
        '
        'GridColumn286
        '
        Me.GridColumn286.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn286.Caption = "Código"
        Me.GridColumn286.FieldName = "Number"
        Me.GridColumn286.Name = "GridColumn286"
        Me.GridColumn286.Width = 300
        '
        'GridColumn287
        '
        Me.GridColumn287.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn287.Caption = "Nombre"
        Me.GridColumn287.FieldName = "Name"
        Me.GridColumn287.Name = "GridColumn287"
        Me.GridColumn287.Width = 350
        '
        'GridColumn288
        '
        Me.GridColumn288.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn288.Caption = "Maneja Tercero"
        Me.GridColumn288.FieldName = "HandlesThirdParty"
        Me.GridColumn288.Name = "GridColumn288"
        Me.GridColumn288.Width = 350
        '
        'GridColumn289
        '
        Me.GridColumn289.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn289.Caption = "Maneja Centro de Costo"
        Me.GridColumn289.FieldName = "HandlesCostCenter"
        Me.GridColumn289.Name = "GridColumn289"
        Me.GridColumn289.Width = 350
        '
        'GridColumn290
        '
        Me.GridColumn290.Caption = "Id"
        Me.GridColumn290.FieldName = "Id"
        Me.GridColumn290.Name = "GridColumn290"
        '
        'GridColumn291
        '
        Me.GridColumn291.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn291.Caption = "Código"
        Me.GridColumn291.FieldName = "Number"
        Me.GridColumn291.Name = "GridColumn291"
        Me.GridColumn291.Width = 300
        '
        'GridColumn292
        '
        Me.GridColumn292.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn292.Caption = "Nombre"
        Me.GridColumn292.FieldName = "Name"
        Me.GridColumn292.Name = "GridColumn292"
        Me.GridColumn292.Width = 350
        '
        'GridColumn293
        '
        Me.GridColumn293.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn293.Caption = "Maneja Tercero"
        Me.GridColumn293.FieldName = "HandlesThirdParty"
        Me.GridColumn293.Name = "GridColumn293"
        Me.GridColumn293.Width = 350
        '
        'GridColumn294
        '
        Me.GridColumn294.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn294.Caption = "Maneja Centro de Costo"
        Me.GridColumn294.FieldName = "HandlesCostCenter"
        Me.GridColumn294.Name = "GridColumn294"
        Me.GridColumn294.Width = 350
        '
        'GridColumn295
        '
        Me.GridColumn295.Caption = "Id"
        Me.GridColumn295.FieldName = "Id"
        Me.GridColumn295.Name = "GridColumn295"
        '
        'GridColumn296
        '
        Me.GridColumn296.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn296.Caption = "Código"
        Me.GridColumn296.FieldName = "Number"
        Me.GridColumn296.Name = "GridColumn296"
        Me.GridColumn296.Width = 300
        '
        'GridColumn297
        '
        Me.GridColumn297.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn297.Caption = "Nombre"
        Me.GridColumn297.FieldName = "Name"
        Me.GridColumn297.Name = "GridColumn297"
        Me.GridColumn297.Width = 350
        '
        'GridColumn298
        '
        Me.GridColumn298.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn298.Caption = "Maneja Tercero"
        Me.GridColumn298.FieldName = "HandlesThirdParty"
        Me.GridColumn298.Name = "GridColumn298"
        Me.GridColumn298.Width = 350
        '
        'GridColumn299
        '
        Me.GridColumn299.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn299.Caption = "Maneja Centro de Costo"
        Me.GridColumn299.FieldName = "HandlesCostCenter"
        Me.GridColumn299.Name = "GridColumn299"
        Me.GridColumn299.Width = 350
        '
        'GridColumn300
        '
        Me.GridColumn300.Caption = "Id"
        Me.GridColumn300.FieldName = "Id"
        Me.GridColumn300.Name = "GridColumn300"
        '
        'GridColumn301
        '
        Me.GridColumn301.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn301.Caption = "Código"
        Me.GridColumn301.FieldName = "Number"
        Me.GridColumn301.Name = "GridColumn301"
        Me.GridColumn301.Width = 300
        '
        'GridColumn302
        '
        Me.GridColumn302.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn302.Caption = "Nombre"
        Me.GridColumn302.FieldName = "Name"
        Me.GridColumn302.Name = "GridColumn302"
        Me.GridColumn302.Width = 350
        '
        'GridColumn303
        '
        Me.GridColumn303.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn303.Caption = "Maneja Tercero"
        Me.GridColumn303.FieldName = "HandlesThirdParty"
        Me.GridColumn303.Name = "GridColumn303"
        Me.GridColumn303.Width = 350
        '
        'GridColumn304
        '
        Me.GridColumn304.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn304.Caption = "Maneja Centro de Costo"
        Me.GridColumn304.FieldName = "HandlesCostCenter"
        Me.GridColumn304.Name = "GridColumn304"
        Me.GridColumn304.Width = 350
        '
        'GridColumn305
        '
        Me.GridColumn305.Caption = "Id"
        Me.GridColumn305.FieldName = "Id"
        Me.GridColumn305.Name = "GridColumn305"
        '
        'GridColumn306
        '
        Me.GridColumn306.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn306.Caption = "Código"
        Me.GridColumn306.FieldName = "Number"
        Me.GridColumn306.Name = "GridColumn306"
        Me.GridColumn306.Width = 300
        '
        'GridColumn307
        '
        Me.GridColumn307.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn307.Caption = "Nombre"
        Me.GridColumn307.FieldName = "Name"
        Me.GridColumn307.Name = "GridColumn307"
        Me.GridColumn307.Width = 350
        '
        'GridColumn308
        '
        Me.GridColumn308.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn308.Caption = "Maneja Tercero"
        Me.GridColumn308.FieldName = "HandlesThirdParty"
        Me.GridColumn308.Name = "GridColumn308"
        Me.GridColumn308.Width = 350
        '
        'GridColumn309
        '
        Me.GridColumn309.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn309.Caption = "Maneja Centro de Costo"
        Me.GridColumn309.FieldName = "HandlesCostCenter"
        Me.GridColumn309.Name = "GridColumn309"
        Me.GridColumn309.Width = 350
        '
        'GridColumn310
        '
        Me.GridColumn310.Caption = "Id"
        Me.GridColumn310.FieldName = "Id"
        Me.GridColumn310.Name = "GridColumn310"
        '
        'GridColumn311
        '
        Me.GridColumn311.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn311.Caption = "Código"
        Me.GridColumn311.FieldName = "Number"
        Me.GridColumn311.Name = "GridColumn311"
        Me.GridColumn311.Width = 300
        '
        'GridColumn312
        '
        Me.GridColumn312.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn312.Caption = "Nombre"
        Me.GridColumn312.FieldName = "Name"
        Me.GridColumn312.Name = "GridColumn312"
        Me.GridColumn312.Width = 350
        '
        'GridColumn313
        '
        Me.GridColumn313.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn313.Caption = "Maneja Tercero"
        Me.GridColumn313.FieldName = "HandlesThirdParty"
        Me.GridColumn313.Name = "GridColumn313"
        Me.GridColumn313.Width = 350
        '
        'GridColumn314
        '
        Me.GridColumn314.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn314.Caption = "Maneja Centro de Costo"
        Me.GridColumn314.FieldName = "HandlesCostCenter"
        Me.GridColumn314.Name = "GridColumn314"
        Me.GridColumn314.Width = 350
        '
        'GridColumn235
        '
        Me.GridColumn235.Caption = "Id"
        Me.GridColumn235.FieldName = "Id"
        Me.GridColumn235.Name = "GridColumn235"
        '
        'GridColumn236
        '
        Me.GridColumn236.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn236.Caption = "Código"
        Me.GridColumn236.FieldName = "Number"
        Me.GridColumn236.Name = "GridColumn236"
        Me.GridColumn236.Width = 300
        '
        'GridColumn237
        '
        Me.GridColumn237.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn237.Caption = "Nombre"
        Me.GridColumn237.FieldName = "Name"
        Me.GridColumn237.Name = "GridColumn237"
        Me.GridColumn237.Width = 350
        '
        'GridColumn238
        '
        Me.GridColumn238.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn238.Caption = "Maneja Tercero"
        Me.GridColumn238.FieldName = "HandlesThirdParty"
        Me.GridColumn238.Name = "GridColumn238"
        Me.GridColumn238.Width = 350
        '
        'GridColumn239
        '
        Me.GridColumn239.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn239.Caption = "Maneja Centro de Costo"
        Me.GridColumn239.FieldName = "HandlesCostCenter"
        Me.GridColumn239.Name = "GridColumn239"
        Me.GridColumn239.Width = 350
        '
        'GridColumn240
        '
        Me.GridColumn240.Caption = "Id"
        Me.GridColumn240.FieldName = "Id"
        Me.GridColumn240.Name = "GridColumn240"
        '
        'GridColumn241
        '
        Me.GridColumn241.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn241.Caption = "Código"
        Me.GridColumn241.FieldName = "Number"
        Me.GridColumn241.Name = "GridColumn241"
        Me.GridColumn241.Width = 300
        '
        'GridColumn242
        '
        Me.GridColumn242.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn242.Caption = "Nombre"
        Me.GridColumn242.FieldName = "Name"
        Me.GridColumn242.Name = "GridColumn242"
        Me.GridColumn242.Width = 350
        '
        'GridColumn243
        '
        Me.GridColumn243.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn243.Caption = "Maneja Tercero"
        Me.GridColumn243.FieldName = "HandlesThirdParty"
        Me.GridColumn243.Name = "GridColumn243"
        Me.GridColumn243.Width = 350
        '
        'GridColumn244
        '
        Me.GridColumn244.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn244.Caption = "Maneja Centro de Costo"
        Me.GridColumn244.FieldName = "HandlesCostCenter"
        Me.GridColumn244.Name = "GridColumn244"
        Me.GridColumn244.Width = 350
        '
        'GridColumn245
        '
        Me.GridColumn245.Caption = "Id"
        Me.GridColumn245.FieldName = "Id"
        Me.GridColumn245.Name = "GridColumn245"
        '
        'GridColumn246
        '
        Me.GridColumn246.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn246.Caption = "Código"
        Me.GridColumn246.FieldName = "Number"
        Me.GridColumn246.Name = "GridColumn246"
        Me.GridColumn246.Width = 300
        '
        'GridColumn247
        '
        Me.GridColumn247.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn247.Caption = "Nombre"
        Me.GridColumn247.FieldName = "Name"
        Me.GridColumn247.Name = "GridColumn247"
        Me.GridColumn247.Width = 350
        '
        'GridColumn248
        '
        Me.GridColumn248.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn248.Caption = "Maneja Tercero"
        Me.GridColumn248.FieldName = "HandlesThirdParty"
        Me.GridColumn248.Name = "GridColumn248"
        Me.GridColumn248.Width = 350
        '
        'GridColumn249
        '
        Me.GridColumn249.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn249.Caption = "Maneja Centro de Costo"
        Me.GridColumn249.FieldName = "HandlesCostCenter"
        Me.GridColumn249.Name = "GridColumn249"
        Me.GridColumn249.Width = 350
        '
        'GridColumn250
        '
        Me.GridColumn250.Caption = "Id"
        Me.GridColumn250.FieldName = "Id"
        Me.GridColumn250.Name = "GridColumn250"
        '
        'GridColumn251
        '
        Me.GridColumn251.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn251.Caption = "Código"
        Me.GridColumn251.FieldName = "Number"
        Me.GridColumn251.Name = "GridColumn251"
        Me.GridColumn251.Width = 300
        '
        'GridColumn252
        '
        Me.GridColumn252.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn252.Caption = "Nombre"
        Me.GridColumn252.FieldName = "Name"
        Me.GridColumn252.Name = "GridColumn252"
        Me.GridColumn252.Width = 350
        '
        'GridColumn253
        '
        Me.GridColumn253.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn253.Caption = "Maneja Tercero"
        Me.GridColumn253.FieldName = "HandlesThirdParty"
        Me.GridColumn253.Name = "GridColumn253"
        Me.GridColumn253.Width = 350
        '
        'GridColumn254
        '
        Me.GridColumn254.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn254.Caption = "Maneja Centro de Costo"
        Me.GridColumn254.FieldName = "HandlesCostCenter"
        Me.GridColumn254.Name = "GridColumn254"
        Me.GridColumn254.Width = 350
        '
        'GridColumn255
        '
        Me.GridColumn255.Caption = "Id"
        Me.GridColumn255.FieldName = "Id"
        Me.GridColumn255.Name = "GridColumn255"
        '
        'GridColumn256
        '
        Me.GridColumn256.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn256.Caption = "Código"
        Me.GridColumn256.FieldName = "Number"
        Me.GridColumn256.Name = "GridColumn256"
        Me.GridColumn256.Width = 300
        '
        'GridColumn257
        '
        Me.GridColumn257.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn257.Caption = "Nombre"
        Me.GridColumn257.FieldName = "Name"
        Me.GridColumn257.Name = "GridColumn257"
        Me.GridColumn257.Width = 350
        '
        'GridColumn258
        '
        Me.GridColumn258.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn258.Caption = "Maneja Tercero"
        Me.GridColumn258.FieldName = "HandlesThirdParty"
        Me.GridColumn258.Name = "GridColumn258"
        Me.GridColumn258.Width = 350
        '
        'GridColumn259
        '
        Me.GridColumn259.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn259.Caption = "Maneja Centro de Costo"
        Me.GridColumn259.FieldName = "HandlesCostCenter"
        Me.GridColumn259.Name = "GridColumn259"
        Me.GridColumn259.Width = 350
        '
        'GridColumn260
        '
        Me.GridColumn260.Caption = "Id"
        Me.GridColumn260.FieldName = "Id"
        Me.GridColumn260.Name = "GridColumn260"
        '
        'GridColumn261
        '
        Me.GridColumn261.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn261.Caption = "Código"
        Me.GridColumn261.FieldName = "Number"
        Me.GridColumn261.Name = "GridColumn261"
        Me.GridColumn261.Width = 300
        '
        'GridColumn262
        '
        Me.GridColumn262.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn262.Caption = "Nombre"
        Me.GridColumn262.FieldName = "Name"
        Me.GridColumn262.Name = "GridColumn262"
        Me.GridColumn262.Width = 350
        '
        'GridColumn263
        '
        Me.GridColumn263.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn263.Caption = "Maneja Tercero"
        Me.GridColumn263.FieldName = "HandlesThirdParty"
        Me.GridColumn263.Name = "GridColumn263"
        Me.GridColumn263.Width = 350
        '
        'GridColumn264
        '
        Me.GridColumn264.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn264.Caption = "Maneja Centro de Costo"
        Me.GridColumn264.FieldName = "HandlesCostCenter"
        Me.GridColumn264.Name = "GridColumn264"
        Me.GridColumn264.Width = 350
        '
        'GridColumn265
        '
        Me.GridColumn265.Caption = "Id"
        Me.GridColumn265.FieldName = "Id"
        Me.GridColumn265.Name = "GridColumn265"
        '
        'GridColumn266
        '
        Me.GridColumn266.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn266.Caption = "Código"
        Me.GridColumn266.FieldName = "Number"
        Me.GridColumn266.Name = "GridColumn266"
        Me.GridColumn266.Width = 300
        '
        'GridColumn267
        '
        Me.GridColumn267.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn267.Caption = "Nombre"
        Me.GridColumn267.FieldName = "Name"
        Me.GridColumn267.Name = "GridColumn267"
        Me.GridColumn267.Width = 350
        '
        'GridColumn268
        '
        Me.GridColumn268.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn268.Caption = "Maneja Tercero"
        Me.GridColumn268.FieldName = "HandlesThirdParty"
        Me.GridColumn268.Name = "GridColumn268"
        Me.GridColumn268.Width = 350
        '
        'GridColumn269
        '
        Me.GridColumn269.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn269.Caption = "Maneja Centro de Costo"
        Me.GridColumn269.FieldName = "HandlesCostCenter"
        Me.GridColumn269.Name = "GridColumn269"
        Me.GridColumn269.Width = 350
        '
        'GridColumn270
        '
        Me.GridColumn270.Caption = "Id"
        Me.GridColumn270.FieldName = "Id"
        Me.GridColumn270.Name = "GridColumn270"
        '
        'GridColumn271
        '
        Me.GridColumn271.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn271.Caption = "Código"
        Me.GridColumn271.FieldName = "Number"
        Me.GridColumn271.Name = "GridColumn271"
        Me.GridColumn271.Width = 300
        '
        'GridColumn272
        '
        Me.GridColumn272.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn272.Caption = "Nombre"
        Me.GridColumn272.FieldName = "Name"
        Me.GridColumn272.Name = "GridColumn272"
        Me.GridColumn272.Width = 350
        '
        'GridColumn273
        '
        Me.GridColumn273.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn273.Caption = "Maneja Tercero"
        Me.GridColumn273.FieldName = "HandlesThirdParty"
        Me.GridColumn273.Name = "GridColumn273"
        Me.GridColumn273.Width = 350
        '
        'GridColumn274
        '
        Me.GridColumn274.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn274.Caption = "Maneja Centro de Costo"
        Me.GridColumn274.FieldName = "HandlesCostCenter"
        Me.GridColumn274.Name = "GridColumn274"
        Me.GridColumn274.Width = 350
        '
        'GridColumn195
        '
        Me.GridColumn195.Caption = "Id"
        Me.GridColumn195.FieldName = "Id"
        Me.GridColumn195.Name = "GridColumn195"
        '
        'GridColumn196
        '
        Me.GridColumn196.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn196.Caption = "Código"
        Me.GridColumn196.FieldName = "Number"
        Me.GridColumn196.Name = "GridColumn196"
        Me.GridColumn196.Width = 300
        '
        'GridColumn197
        '
        Me.GridColumn197.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn197.Caption = "Nombre"
        Me.GridColumn197.FieldName = "Name"
        Me.GridColumn197.Name = "GridColumn197"
        Me.GridColumn197.Width = 350
        '
        'GridColumn198
        '
        Me.GridColumn198.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn198.Caption = "Maneja Tercero"
        Me.GridColumn198.FieldName = "HandlesThirdParty"
        Me.GridColumn198.Name = "GridColumn198"
        Me.GridColumn198.Width = 350
        '
        'GridColumn199
        '
        Me.GridColumn199.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn199.Caption = "Maneja Centro de Costo"
        Me.GridColumn199.FieldName = "HandlesCostCenter"
        Me.GridColumn199.Name = "GridColumn199"
        Me.GridColumn199.Width = 350
        '
        'GridColumn200
        '
        Me.GridColumn200.Caption = "Id"
        Me.GridColumn200.FieldName = "Id"
        Me.GridColumn200.Name = "GridColumn200"
        '
        'GridColumn201
        '
        Me.GridColumn201.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn201.Caption = "Código"
        Me.GridColumn201.FieldName = "Number"
        Me.GridColumn201.Name = "GridColumn201"
        Me.GridColumn201.Width = 300
        '
        'GridColumn202
        '
        Me.GridColumn202.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn202.Caption = "Nombre"
        Me.GridColumn202.FieldName = "Name"
        Me.GridColumn202.Name = "GridColumn202"
        Me.GridColumn202.Width = 350
        '
        'GridColumn203
        '
        Me.GridColumn203.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn203.Caption = "Maneja Tercero"
        Me.GridColumn203.FieldName = "HandlesThirdParty"
        Me.GridColumn203.Name = "GridColumn203"
        Me.GridColumn203.Width = 350
        '
        'GridColumn204
        '
        Me.GridColumn204.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn204.Caption = "Maneja Centro de Costo"
        Me.GridColumn204.FieldName = "HandlesCostCenter"
        Me.GridColumn204.Name = "GridColumn204"
        Me.GridColumn204.Width = 350
        '
        'GridColumn205
        '
        Me.GridColumn205.Caption = "Id"
        Me.GridColumn205.FieldName = "Id"
        Me.GridColumn205.Name = "GridColumn205"
        '
        'GridColumn206
        '
        Me.GridColumn206.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn206.Caption = "Código"
        Me.GridColumn206.FieldName = "Number"
        Me.GridColumn206.Name = "GridColumn206"
        Me.GridColumn206.Width = 300
        '
        'GridColumn207
        '
        Me.GridColumn207.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn207.Caption = "Nombre"
        Me.GridColumn207.FieldName = "Name"
        Me.GridColumn207.Name = "GridColumn207"
        Me.GridColumn207.Width = 350
        '
        'GridColumn208
        '
        Me.GridColumn208.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn208.Caption = "Maneja Tercero"
        Me.GridColumn208.FieldName = "HandlesThirdParty"
        Me.GridColumn208.Name = "GridColumn208"
        Me.GridColumn208.Width = 350
        '
        'GridColumn209
        '
        Me.GridColumn209.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn209.Caption = "Maneja Centro de Costo"
        Me.GridColumn209.FieldName = "HandlesCostCenter"
        Me.GridColumn209.Name = "GridColumn209"
        Me.GridColumn209.Width = 350
        '
        'GridColumn210
        '
        Me.GridColumn210.Caption = "Id"
        Me.GridColumn210.FieldName = "Id"
        Me.GridColumn210.Name = "GridColumn210"
        '
        'GridColumn211
        '
        Me.GridColumn211.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn211.Caption = "Código"
        Me.GridColumn211.FieldName = "Number"
        Me.GridColumn211.Name = "GridColumn211"
        Me.GridColumn211.Width = 300
        '
        'GridColumn212
        '
        Me.GridColumn212.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn212.Caption = "Nombre"
        Me.GridColumn212.FieldName = "Name"
        Me.GridColumn212.Name = "GridColumn212"
        Me.GridColumn212.Width = 350
        '
        'GridColumn213
        '
        Me.GridColumn213.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn213.Caption = "Maneja Tercero"
        Me.GridColumn213.FieldName = "HandlesThirdParty"
        Me.GridColumn213.Name = "GridColumn213"
        Me.GridColumn213.Width = 350
        '
        'GridColumn214
        '
        Me.GridColumn214.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn214.Caption = "Maneja Centro de Costo"
        Me.GridColumn214.FieldName = "HandlesCostCenter"
        Me.GridColumn214.Name = "GridColumn214"
        Me.GridColumn214.Width = 350
        '
        'GridColumn215
        '
        Me.GridColumn215.Caption = "Id"
        Me.GridColumn215.FieldName = "Id"
        Me.GridColumn215.Name = "GridColumn215"
        '
        'GridColumn216
        '
        Me.GridColumn216.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn216.Caption = "Código"
        Me.GridColumn216.FieldName = "Number"
        Me.GridColumn216.Name = "GridColumn216"
        Me.GridColumn216.Width = 300
        '
        'GridColumn217
        '
        Me.GridColumn217.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn217.Caption = "Nombre"
        Me.GridColumn217.FieldName = "Name"
        Me.GridColumn217.Name = "GridColumn217"
        Me.GridColumn217.Width = 350
        '
        'GridColumn218
        '
        Me.GridColumn218.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn218.Caption = "Maneja Tercero"
        Me.GridColumn218.FieldName = "HandlesThirdParty"
        Me.GridColumn218.Name = "GridColumn218"
        Me.GridColumn218.Width = 350
        '
        'GridColumn219
        '
        Me.GridColumn219.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn219.Caption = "Maneja Centro de Costo"
        Me.GridColumn219.FieldName = "HandlesCostCenter"
        Me.GridColumn219.Name = "GridColumn219"
        Me.GridColumn219.Width = 350
        '
        'GridColumn220
        '
        Me.GridColumn220.Caption = "Id"
        Me.GridColumn220.FieldName = "Id"
        Me.GridColumn220.Name = "GridColumn220"
        '
        'GridColumn221
        '
        Me.GridColumn221.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn221.Caption = "Código"
        Me.GridColumn221.FieldName = "Number"
        Me.GridColumn221.Name = "GridColumn221"
        Me.GridColumn221.Width = 300
        '
        'GridColumn222
        '
        Me.GridColumn222.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn222.Caption = "Nombre"
        Me.GridColumn222.FieldName = "Name"
        Me.GridColumn222.Name = "GridColumn222"
        Me.GridColumn222.Width = 350
        '
        'GridColumn223
        '
        Me.GridColumn223.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn223.Caption = "Maneja Tercero"
        Me.GridColumn223.FieldName = "HandlesThirdParty"
        Me.GridColumn223.Name = "GridColumn223"
        Me.GridColumn223.Width = 350
        '
        'GridColumn224
        '
        Me.GridColumn224.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn224.Caption = "Maneja Centro de Costo"
        Me.GridColumn224.FieldName = "HandlesCostCenter"
        Me.GridColumn224.Name = "GridColumn224"
        Me.GridColumn224.Width = 350
        '
        'GridColumn225
        '
        Me.GridColumn225.Caption = "Id"
        Me.GridColumn225.FieldName = "Id"
        Me.GridColumn225.Name = "GridColumn225"
        '
        'GridColumn226
        '
        Me.GridColumn226.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn226.Caption = "Código"
        Me.GridColumn226.FieldName = "Number"
        Me.GridColumn226.Name = "GridColumn226"
        Me.GridColumn226.Width = 300
        '
        'GridColumn227
        '
        Me.GridColumn227.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn227.Caption = "Nombre"
        Me.GridColumn227.FieldName = "Name"
        Me.GridColumn227.Name = "GridColumn227"
        Me.GridColumn227.Width = 350
        '
        'GridColumn228
        '
        Me.GridColumn228.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn228.Caption = "Maneja Tercero"
        Me.GridColumn228.FieldName = "HandlesThirdParty"
        Me.GridColumn228.Name = "GridColumn228"
        Me.GridColumn228.Width = 350
        '
        'GridColumn229
        '
        Me.GridColumn229.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn229.Caption = "Maneja Centro de Costo"
        Me.GridColumn229.FieldName = "HandlesCostCenter"
        Me.GridColumn229.Name = "GridColumn229"
        Me.GridColumn229.Width = 350
        '
        'GridColumn230
        '
        Me.GridColumn230.Caption = "Id"
        Me.GridColumn230.FieldName = "Id"
        Me.GridColumn230.Name = "GridColumn230"
        '
        'GridColumn231
        '
        Me.GridColumn231.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn231.Caption = "Código"
        Me.GridColumn231.FieldName = "Number"
        Me.GridColumn231.Name = "GridColumn231"
        Me.GridColumn231.Width = 300
        '
        'GridColumn232
        '
        Me.GridColumn232.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn232.Caption = "Nombre"
        Me.GridColumn232.FieldName = "Name"
        Me.GridColumn232.Name = "GridColumn232"
        Me.GridColumn232.Width = 350
        '
        'GridColumn233
        '
        Me.GridColumn233.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn233.Caption = "Maneja Tercero"
        Me.GridColumn233.FieldName = "HandlesThirdParty"
        Me.GridColumn233.Name = "GridColumn233"
        Me.GridColumn233.Width = 350
        '
        'GridColumn234
        '
        Me.GridColumn234.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn234.Caption = "Maneja Centro de Costo"
        Me.GridColumn234.FieldName = "HandlesCostCenter"
        Me.GridColumn234.Name = "GridColumn234"
        Me.GridColumn234.Width = 350
        '
        'GridColumn171
        '
        Me.GridColumn171.Caption = "Id"
        Me.GridColumn171.FieldName = "Id"
        Me.GridColumn171.Name = "GridColumn171"
        '
        'GridColumn172
        '
        Me.GridColumn172.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn172.Caption = "Código"
        Me.GridColumn172.FieldName = "Number"
        Me.GridColumn172.Name = "GridColumn172"
        Me.GridColumn172.Width = 294
        '
        'GridColumn173
        '
        Me.GridColumn173.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn173.Caption = "Nombre"
        Me.GridColumn173.FieldName = "Name"
        Me.GridColumn173.Name = "GridColumn173"
        Me.GridColumn173.Width = 1098
        '
        'GridColumn174
        '
        Me.GridColumn174.Caption = "Id"
        Me.GridColumn174.FieldName = "Id"
        Me.GridColumn174.Name = "GridColumn174"
        '
        'GridColumn175
        '
        Me.GridColumn175.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn175.Caption = "Código"
        Me.GridColumn175.FieldName = "Number"
        Me.GridColumn175.Name = "GridColumn175"
        Me.GridColumn175.Width = 294
        '
        'GridColumn176
        '
        Me.GridColumn176.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn176.Caption = "Nombre"
        Me.GridColumn176.FieldName = "Name"
        Me.GridColumn176.Name = "GridColumn176"
        Me.GridColumn176.Width = 1098
        '
        'GridColumn177
        '
        Me.GridColumn177.Caption = "Id"
        Me.GridColumn177.FieldName = "Id"
        Me.GridColumn177.Name = "GridColumn177"
        '
        'GridColumn178
        '
        Me.GridColumn178.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn178.Caption = "Código"
        Me.GridColumn178.FieldName = "Number"
        Me.GridColumn178.Name = "GridColumn178"
        Me.GridColumn178.Width = 294
        '
        'GridColumn179
        '
        Me.GridColumn179.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn179.Caption = "Nombre"
        Me.GridColumn179.FieldName = "Name"
        Me.GridColumn179.Name = "GridColumn179"
        Me.GridColumn179.Width = 1098
        '
        'GridColumn180
        '
        Me.GridColumn180.Caption = "Id"
        Me.GridColumn180.FieldName = "Id"
        Me.GridColumn180.Name = "GridColumn180"
        '
        'GridColumn181
        '
        Me.GridColumn181.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn181.Caption = "Código"
        Me.GridColumn181.FieldName = "Number"
        Me.GridColumn181.Name = "GridColumn181"
        Me.GridColumn181.Width = 294
        '
        'GridColumn182
        '
        Me.GridColumn182.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn182.Caption = "Nombre"
        Me.GridColumn182.FieldName = "Name"
        Me.GridColumn182.Name = "GridColumn182"
        Me.GridColumn182.Width = 1098
        '
        'GridColumn183
        '
        Me.GridColumn183.Caption = "Id"
        Me.GridColumn183.FieldName = "Id"
        Me.GridColumn183.Name = "GridColumn183"
        '
        'GridColumn184
        '
        Me.GridColumn184.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn184.Caption = "Código"
        Me.GridColumn184.FieldName = "Number"
        Me.GridColumn184.Name = "GridColumn184"
        Me.GridColumn184.Width = 294
        '
        'GridColumn185
        '
        Me.GridColumn185.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn185.Caption = "Nombre"
        Me.GridColumn185.FieldName = "Name"
        Me.GridColumn185.Name = "GridColumn185"
        Me.GridColumn185.Width = 1098
        '
        'GridColumn186
        '
        Me.GridColumn186.Caption = "Id"
        Me.GridColumn186.FieldName = "Id"
        Me.GridColumn186.Name = "GridColumn186"
        '
        'GridColumn187
        '
        Me.GridColumn187.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn187.Caption = "Código"
        Me.GridColumn187.FieldName = "Number"
        Me.GridColumn187.Name = "GridColumn187"
        Me.GridColumn187.Width = 294
        '
        'GridColumn188
        '
        Me.GridColumn188.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn188.Caption = "Nombre"
        Me.GridColumn188.FieldName = "Name"
        Me.GridColumn188.Name = "GridColumn188"
        Me.GridColumn188.Width = 1098
        '
        'GridColumn189
        '
        Me.GridColumn189.Caption = "Id"
        Me.GridColumn189.FieldName = "Id"
        Me.GridColumn189.Name = "GridColumn189"
        '
        'GridColumn190
        '
        Me.GridColumn190.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn190.Caption = "Código"
        Me.GridColumn190.FieldName = "Number"
        Me.GridColumn190.Name = "GridColumn190"
        Me.GridColumn190.Width = 294
        '
        'GridColumn191
        '
        Me.GridColumn191.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn191.Caption = "Nombre"
        Me.GridColumn191.FieldName = "Name"
        Me.GridColumn191.Name = "GridColumn191"
        Me.GridColumn191.Width = 1098
        '
        'GridColumn192
        '
        Me.GridColumn192.Caption = "Id"
        Me.GridColumn192.FieldName = "Id"
        Me.GridColumn192.Name = "GridColumn192"
        '
        'GridColumn193
        '
        Me.GridColumn193.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn193.Caption = "Código"
        Me.GridColumn193.FieldName = "Number"
        Me.GridColumn193.Name = "GridColumn193"
        Me.GridColumn193.Width = 294
        '
        'GridColumn194
        '
        Me.GridColumn194.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn194.Caption = "Nombre"
        Me.GridColumn194.FieldName = "Name"
        Me.GridColumn194.Name = "GridColumn194"
        Me.GridColumn194.Width = 1098
        '
        'GridColumn147
        '
        Me.GridColumn147.Caption = "Id"
        Me.GridColumn147.FieldName = "Id"
        Me.GridColumn147.Name = "GridColumn147"
        '
        'GridColumn148
        '
        Me.GridColumn148.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn148.Caption = "Código"
        Me.GridColumn148.FieldName = "AccountCode"
        Me.GridColumn148.Name = "GridColumn148"
        Me.GridColumn148.Width = 294
        '
        'GridColumn149
        '
        Me.GridColumn149.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn149.Caption = "Nombre"
        Me.GridColumn149.FieldName = "AccountName"
        Me.GridColumn149.Name = "GridColumn149"
        Me.GridColumn149.Width = 1098
        '
        'GridColumn150
        '
        Me.GridColumn150.Caption = "Id"
        Me.GridColumn150.FieldName = "Id"
        Me.GridColumn150.Name = "GridColumn150"
        '
        'GridColumn151
        '
        Me.GridColumn151.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn151.Caption = "Código"
        Me.GridColumn151.FieldName = "AccountCode"
        Me.GridColumn151.Name = "GridColumn151"
        Me.GridColumn151.Width = 294
        '
        'GridColumn152
        '
        Me.GridColumn152.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn152.Caption = "Nombre"
        Me.GridColumn152.FieldName = "AccountName"
        Me.GridColumn152.Name = "GridColumn152"
        Me.GridColumn152.Width = 1098
        '
        'GridColumn153
        '
        Me.GridColumn153.Caption = "Id"
        Me.GridColumn153.FieldName = "Id"
        Me.GridColumn153.Name = "GridColumn153"
        '
        'GridColumn154
        '
        Me.GridColumn154.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn154.Caption = "Código"
        Me.GridColumn154.FieldName = "AccountCode"
        Me.GridColumn154.Name = "GridColumn154"
        Me.GridColumn154.Width = 294
        '
        'GridColumn155
        '
        Me.GridColumn155.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn155.Caption = "Nombre"
        Me.GridColumn155.FieldName = "AccountName"
        Me.GridColumn155.Name = "GridColumn155"
        Me.GridColumn155.Width = 1098
        '
        'GridColumn156
        '
        Me.GridColumn156.Caption = "Id"
        Me.GridColumn156.FieldName = "Id"
        Me.GridColumn156.Name = "GridColumn156"
        '
        'GridColumn157
        '
        Me.GridColumn157.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn157.Caption = "Código"
        Me.GridColumn157.FieldName = "AccountCode"
        Me.GridColumn157.Name = "GridColumn157"
        Me.GridColumn157.Width = 294
        '
        'GridColumn158
        '
        Me.GridColumn158.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn158.Caption = "Nombre"
        Me.GridColumn158.FieldName = "AccountName"
        Me.GridColumn158.Name = "GridColumn158"
        Me.GridColumn158.Width = 1098
        '
        'GridColumn159
        '
        Me.GridColumn159.Caption = "Id"
        Me.GridColumn159.FieldName = "Id"
        Me.GridColumn159.Name = "GridColumn159"
        '
        'GridColumn160
        '
        Me.GridColumn160.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn160.Caption = "Código"
        Me.GridColumn160.FieldName = "AccountCode"
        Me.GridColumn160.Name = "GridColumn160"
        Me.GridColumn160.Width = 294
        '
        'GridColumn161
        '
        Me.GridColumn161.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn161.Caption = "Nombre"
        Me.GridColumn161.FieldName = "AccountName"
        Me.GridColumn161.Name = "GridColumn161"
        Me.GridColumn161.Width = 1098
        '
        'GridColumn162
        '
        Me.GridColumn162.Caption = "Id"
        Me.GridColumn162.FieldName = "Id"
        Me.GridColumn162.Name = "GridColumn162"
        '
        'GridColumn163
        '
        Me.GridColumn163.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn163.Caption = "Código"
        Me.GridColumn163.FieldName = "AccountCode"
        Me.GridColumn163.Name = "GridColumn163"
        Me.GridColumn163.Width = 294
        '
        'GridColumn164
        '
        Me.GridColumn164.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn164.Caption = "Nombre"
        Me.GridColumn164.FieldName = "AccountName"
        Me.GridColumn164.Name = "GridColumn164"
        Me.GridColumn164.Width = 1098
        '
        'GridColumn165
        '
        Me.GridColumn165.Caption = "Id"
        Me.GridColumn165.FieldName = "Id"
        Me.GridColumn165.Name = "GridColumn165"
        '
        'GridColumn166
        '
        Me.GridColumn166.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn166.Caption = "Código"
        Me.GridColumn166.FieldName = "AccountCode"
        Me.GridColumn166.Name = "GridColumn166"
        Me.GridColumn166.Width = 294
        '
        'GridColumn167
        '
        Me.GridColumn167.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn167.Caption = "Nombre"
        Me.GridColumn167.FieldName = "AccountName"
        Me.GridColumn167.Name = "GridColumn167"
        Me.GridColumn167.Width = 1098
        '
        'GridColumn168
        '
        Me.GridColumn168.Caption = "Id"
        Me.GridColumn168.FieldName = "Id"
        Me.GridColumn168.Name = "GridColumn168"
        '
        'GridColumn169
        '
        Me.GridColumn169.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn169.Caption = "Código"
        Me.GridColumn169.FieldName = "AccountCode"
        Me.GridColumn169.Name = "GridColumn169"
        Me.GridColumn169.Width = 294
        '
        'GridColumn170
        '
        Me.GridColumn170.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn170.Caption = "Nombre"
        Me.GridColumn170.FieldName = "AccountName"
        Me.GridColumn170.Name = "GridColumn170"
        Me.GridColumn170.Width = 1098
        '
        'GridColumn123
        '
        Me.GridColumn123.Caption = "Id"
        Me.GridColumn123.FieldName = "Id"
        Me.GridColumn123.Name = "GridColumn123"
        '
        'GridColumn124
        '
        Me.GridColumn124.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn124.Caption = "Código"
        Me.GridColumn124.FieldName = "AccountCode"
        Me.GridColumn124.Name = "GridColumn124"
        Me.GridColumn124.Width = 294
        '
        'GridColumn125
        '
        Me.GridColumn125.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn125.Caption = "Nombre"
        Me.GridColumn125.FieldName = "AccountName"
        Me.GridColumn125.Name = "GridColumn125"
        Me.GridColumn125.Width = 1098
        '
        'GridColumn126
        '
        Me.GridColumn126.Caption = "Id"
        Me.GridColumn126.FieldName = "Id"
        Me.GridColumn126.Name = "GridColumn126"
        '
        'GridColumn127
        '
        Me.GridColumn127.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn127.Caption = "Código"
        Me.GridColumn127.FieldName = "AccountCode"
        Me.GridColumn127.Name = "GridColumn127"
        Me.GridColumn127.Width = 294
        '
        'GridColumn128
        '
        Me.GridColumn128.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn128.Caption = "Nombre"
        Me.GridColumn128.FieldName = "AccountName"
        Me.GridColumn128.Name = "GridColumn128"
        Me.GridColumn128.Width = 1098
        '
        'GridColumn129
        '
        Me.GridColumn129.Caption = "Id"
        Me.GridColumn129.FieldName = "Id"
        Me.GridColumn129.Name = "GridColumn129"
        '
        'GridColumn130
        '
        Me.GridColumn130.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn130.Caption = "Código"
        Me.GridColumn130.FieldName = "AccountCode"
        Me.GridColumn130.Name = "GridColumn130"
        Me.GridColumn130.Width = 294
        '
        'GridColumn131
        '
        Me.GridColumn131.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn131.Caption = "Nombre"
        Me.GridColumn131.FieldName = "AccountName"
        Me.GridColumn131.Name = "GridColumn131"
        Me.GridColumn131.Width = 1098
        '
        'GridColumn132
        '
        Me.GridColumn132.Caption = "Id"
        Me.GridColumn132.FieldName = "Id"
        Me.GridColumn132.Name = "GridColumn132"
        '
        'GridColumn133
        '
        Me.GridColumn133.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn133.Caption = "Código"
        Me.GridColumn133.FieldName = "AccountCode"
        Me.GridColumn133.Name = "GridColumn133"
        Me.GridColumn133.Width = 294
        '
        'GridColumn134
        '
        Me.GridColumn134.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn134.Caption = "Nombre"
        Me.GridColumn134.FieldName = "AccountName"
        Me.GridColumn134.Name = "GridColumn134"
        Me.GridColumn134.Width = 1098
        '
        'GridColumn135
        '
        Me.GridColumn135.Caption = "Id"
        Me.GridColumn135.FieldName = "Id"
        Me.GridColumn135.Name = "GridColumn135"
        '
        'GridColumn136
        '
        Me.GridColumn136.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn136.Caption = "Código"
        Me.GridColumn136.FieldName = "AccountCode"
        Me.GridColumn136.Name = "GridColumn136"
        Me.GridColumn136.Width = 294
        '
        'GridColumn137
        '
        Me.GridColumn137.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn137.Caption = "Nombre"
        Me.GridColumn137.FieldName = "AccountName"
        Me.GridColumn137.Name = "GridColumn137"
        Me.GridColumn137.Width = 1098
        '
        'GridColumn138
        '
        Me.GridColumn138.Caption = "Id"
        Me.GridColumn138.FieldName = "Id"
        Me.GridColumn138.Name = "GridColumn138"
        '
        'GridColumn139
        '
        Me.GridColumn139.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn139.Caption = "Código"
        Me.GridColumn139.FieldName = "AccountCode"
        Me.GridColumn139.Name = "GridColumn139"
        Me.GridColumn139.Width = 294
        '
        'GridColumn140
        '
        Me.GridColumn140.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn140.Caption = "Nombre"
        Me.GridColumn140.FieldName = "AccountName"
        Me.GridColumn140.Name = "GridColumn140"
        Me.GridColumn140.Width = 1098
        '
        'GridColumn141
        '
        Me.GridColumn141.Caption = "Id"
        Me.GridColumn141.FieldName = "Id"
        Me.GridColumn141.Name = "GridColumn141"
        '
        'GridColumn142
        '
        Me.GridColumn142.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn142.Caption = "Código"
        Me.GridColumn142.FieldName = "AccountCode"
        Me.GridColumn142.Name = "GridColumn142"
        Me.GridColumn142.Width = 294
        '
        'GridColumn143
        '
        Me.GridColumn143.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn143.Caption = "Nombre"
        Me.GridColumn143.FieldName = "AccountName"
        Me.GridColumn143.Name = "GridColumn143"
        Me.GridColumn143.Width = 1098
        '
        'GridColumn144
        '
        Me.GridColumn144.Caption = "Id"
        Me.GridColumn144.FieldName = "Id"
        Me.GridColumn144.Name = "GridColumn144"
        '
        'GridColumn145
        '
        Me.GridColumn145.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn145.Caption = "Código"
        Me.GridColumn145.FieldName = "AccountCode"
        Me.GridColumn145.Name = "GridColumn145"
        Me.GridColumn145.Width = 294
        '
        'GridColumn146
        '
        Me.GridColumn146.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn146.Caption = "Nombre"
        Me.GridColumn146.FieldName = "AccountName"
        Me.GridColumn146.Name = "GridColumn146"
        Me.GridColumn146.Width = 1098
        '
        'GridColumn99
        '
        Me.GridColumn99.Caption = "Id"
        Me.GridColumn99.FieldName = "Id"
        Me.GridColumn99.Name = "GridColumn99"
        '
        'GridColumn100
        '
        Me.GridColumn100.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn100.Caption = "Código"
        Me.GridColumn100.FieldName = "AccountCode"
        Me.GridColumn100.Name = "GridColumn100"
        Me.GridColumn100.Width = 294
        '
        'GridColumn101
        '
        Me.GridColumn101.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn101.Caption = "Nombre"
        Me.GridColumn101.FieldName = "AccountName"
        Me.GridColumn101.Name = "GridColumn101"
        Me.GridColumn101.Width = 1098
        '
        'GridColumn102
        '
        Me.GridColumn102.Caption = "Id"
        Me.GridColumn102.FieldName = "Id"
        Me.GridColumn102.Name = "GridColumn102"
        '
        'GridColumn103
        '
        Me.GridColumn103.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn103.Caption = "Código"
        Me.GridColumn103.FieldName = "AccountCode"
        Me.GridColumn103.Name = "GridColumn103"
        Me.GridColumn103.Width = 294
        '
        'GridColumn104
        '
        Me.GridColumn104.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn104.Caption = "Nombre"
        Me.GridColumn104.FieldName = "AccountName"
        Me.GridColumn104.Name = "GridColumn104"
        Me.GridColumn104.Width = 1098
        '
        'GridColumn105
        '
        Me.GridColumn105.Caption = "Id"
        Me.GridColumn105.FieldName = "Id"
        Me.GridColumn105.Name = "GridColumn105"
        '
        'GridColumn106
        '
        Me.GridColumn106.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn106.Caption = "Código"
        Me.GridColumn106.FieldName = "AccountCode"
        Me.GridColumn106.Name = "GridColumn106"
        Me.GridColumn106.Width = 294
        '
        'GridColumn107
        '
        Me.GridColumn107.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn107.Caption = "Nombre"
        Me.GridColumn107.FieldName = "AccountName"
        Me.GridColumn107.Name = "GridColumn107"
        Me.GridColumn107.Width = 1098
        '
        'GridColumn108
        '
        Me.GridColumn108.Caption = "Id"
        Me.GridColumn108.FieldName = "Id"
        Me.GridColumn108.Name = "GridColumn108"
        '
        'GridColumn109
        '
        Me.GridColumn109.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn109.Caption = "Código"
        Me.GridColumn109.FieldName = "AccountCode"
        Me.GridColumn109.Name = "GridColumn109"
        Me.GridColumn109.Width = 294
        '
        'GridColumn110
        '
        Me.GridColumn110.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn110.Caption = "Nombre"
        Me.GridColumn110.FieldName = "AccountName"
        Me.GridColumn110.Name = "GridColumn110"
        Me.GridColumn110.Width = 1098
        '
        'GridColumn111
        '
        Me.GridColumn111.Caption = "Id"
        Me.GridColumn111.FieldName = "Id"
        Me.GridColumn111.Name = "GridColumn111"
        '
        'GridColumn112
        '
        Me.GridColumn112.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn112.Caption = "Código"
        Me.GridColumn112.FieldName = "AccountCode"
        Me.GridColumn112.Name = "GridColumn112"
        Me.GridColumn112.Width = 294
        '
        'GridColumn113
        '
        Me.GridColumn113.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn113.Caption = "Nombre"
        Me.GridColumn113.FieldName = "AccountName"
        Me.GridColumn113.Name = "GridColumn113"
        Me.GridColumn113.Width = 1098
        '
        'GridColumn114
        '
        Me.GridColumn114.Caption = "Id"
        Me.GridColumn114.FieldName = "Id"
        Me.GridColumn114.Name = "GridColumn114"
        '
        'GridColumn115
        '
        Me.GridColumn115.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn115.Caption = "Código"
        Me.GridColumn115.FieldName = "AccountCode"
        Me.GridColumn115.Name = "GridColumn115"
        Me.GridColumn115.Width = 294
        '
        'GridColumn116
        '
        Me.GridColumn116.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn116.Caption = "Nombre"
        Me.GridColumn116.FieldName = "AccountName"
        Me.GridColumn116.Name = "GridColumn116"
        Me.GridColumn116.Width = 1098
        '
        'GridColumn117
        '
        Me.GridColumn117.Caption = "Id"
        Me.GridColumn117.FieldName = "Id"
        Me.GridColumn117.Name = "GridColumn117"
        '
        'GridColumn118
        '
        Me.GridColumn118.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn118.Caption = "Código"
        Me.GridColumn118.FieldName = "AccountCode"
        Me.GridColumn118.Name = "GridColumn118"
        Me.GridColumn118.Width = 294
        '
        'GridColumn119
        '
        Me.GridColumn119.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn119.Caption = "Nombre"
        Me.GridColumn119.FieldName = "AccountName"
        Me.GridColumn119.Name = "GridColumn119"
        Me.GridColumn119.Width = 1098
        '
        'GridColumn120
        '
        Me.GridColumn120.Caption = "Id"
        Me.GridColumn120.FieldName = "Id"
        Me.GridColumn120.Name = "GridColumn120"
        '
        'GridColumn121
        '
        Me.GridColumn121.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn121.Caption = "Código"
        Me.GridColumn121.FieldName = "AccountCode"
        Me.GridColumn121.Name = "GridColumn121"
        Me.GridColumn121.Width = 294
        '
        'GridColumn122
        '
        Me.GridColumn122.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn122.Caption = "Nombre"
        Me.GridColumn122.FieldName = "AccountName"
        Me.GridColumn122.Name = "GridColumn122"
        Me.GridColumn122.Width = 1098
        '
        'GridColumn75
        '
        Me.GridColumn75.Caption = "Id"
        Me.GridColumn75.FieldName = "Id"
        Me.GridColumn75.Name = "GridColumn75"
        '
        'GridColumn76
        '
        Me.GridColumn76.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn76.Caption = "Código"
        Me.GridColumn76.FieldName = "AccountCode"
        Me.GridColumn76.Name = "GridColumn76"
        Me.GridColumn76.Width = 294
        '
        'GridColumn77
        '
        Me.GridColumn77.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn77.Caption = "Nombre"
        Me.GridColumn77.FieldName = "AccountName"
        Me.GridColumn77.Name = "GridColumn77"
        Me.GridColumn77.Width = 1098
        '
        'GridColumn78
        '
        Me.GridColumn78.Caption = "Id"
        Me.GridColumn78.FieldName = "Id"
        Me.GridColumn78.Name = "GridColumn78"
        '
        'GridColumn79
        '
        Me.GridColumn79.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn79.Caption = "Código"
        Me.GridColumn79.FieldName = "AccountCode"
        Me.GridColumn79.Name = "GridColumn79"
        Me.GridColumn79.Width = 294
        '
        'GridColumn80
        '
        Me.GridColumn80.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn80.Caption = "Nombre"
        Me.GridColumn80.FieldName = "AccountName"
        Me.GridColumn80.Name = "GridColumn80"
        Me.GridColumn80.Width = 1098
        '
        'GridColumn81
        '
        Me.GridColumn81.Caption = "Id"
        Me.GridColumn81.FieldName = "Id"
        Me.GridColumn81.Name = "GridColumn81"
        '
        'GridColumn82
        '
        Me.GridColumn82.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn82.Caption = "Código"
        Me.GridColumn82.FieldName = "AccountCode"
        Me.GridColumn82.Name = "GridColumn82"
        Me.GridColumn82.Width = 294
        '
        'GridColumn83
        '
        Me.GridColumn83.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn83.Caption = "Nombre"
        Me.GridColumn83.FieldName = "AccountName"
        Me.GridColumn83.Name = "GridColumn83"
        Me.GridColumn83.Width = 1098
        '
        'GridColumn84
        '
        Me.GridColumn84.Caption = "Id"
        Me.GridColumn84.FieldName = "Id"
        Me.GridColumn84.Name = "GridColumn84"
        '
        'GridColumn85
        '
        Me.GridColumn85.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn85.Caption = "Código"
        Me.GridColumn85.FieldName = "AccountCode"
        Me.GridColumn85.Name = "GridColumn85"
        Me.GridColumn85.Width = 294
        '
        'GridColumn86
        '
        Me.GridColumn86.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn86.Caption = "Nombre"
        Me.GridColumn86.FieldName = "AccountName"
        Me.GridColumn86.Name = "GridColumn86"
        Me.GridColumn86.Width = 1098
        '
        'GridColumn87
        '
        Me.GridColumn87.Caption = "Id"
        Me.GridColumn87.FieldName = "Id"
        Me.GridColumn87.Name = "GridColumn87"
        '
        'GridColumn88
        '
        Me.GridColumn88.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn88.Caption = "Código"
        Me.GridColumn88.FieldName = "AccountCode"
        Me.GridColumn88.Name = "GridColumn88"
        Me.GridColumn88.Width = 294
        '
        'GridColumn89
        '
        Me.GridColumn89.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn89.Caption = "Nombre"
        Me.GridColumn89.FieldName = "AccountName"
        Me.GridColumn89.Name = "GridColumn89"
        Me.GridColumn89.Width = 1098
        '
        'GridColumn90
        '
        Me.GridColumn90.Caption = "Id"
        Me.GridColumn90.FieldName = "Id"
        Me.GridColumn90.Name = "GridColumn90"
        '
        'GridColumn91
        '
        Me.GridColumn91.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn91.Caption = "Código"
        Me.GridColumn91.FieldName = "AccountCode"
        Me.GridColumn91.Name = "GridColumn91"
        Me.GridColumn91.Width = 294
        '
        'GridColumn92
        '
        Me.GridColumn92.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn92.Caption = "Nombre"
        Me.GridColumn92.FieldName = "AccountName"
        Me.GridColumn92.Name = "GridColumn92"
        Me.GridColumn92.Width = 1098
        '
        'GridColumn93
        '
        Me.GridColumn93.Caption = "Id"
        Me.GridColumn93.FieldName = "Id"
        Me.GridColumn93.Name = "GridColumn93"
        '
        'GridColumn94
        '
        Me.GridColumn94.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn94.Caption = "Código"
        Me.GridColumn94.FieldName = "AccountCode"
        Me.GridColumn94.Name = "GridColumn94"
        Me.GridColumn94.Width = 294
        '
        'GridColumn95
        '
        Me.GridColumn95.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn95.Caption = "Nombre"
        Me.GridColumn95.FieldName = "AccountName"
        Me.GridColumn95.Name = "GridColumn95"
        Me.GridColumn95.Width = 1098
        '
        'GridColumn96
        '
        Me.GridColumn96.Caption = "Id"
        Me.GridColumn96.FieldName = "Id"
        Me.GridColumn96.Name = "GridColumn96"
        '
        'GridColumn97
        '
        Me.GridColumn97.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn97.Caption = "Código"
        Me.GridColumn97.FieldName = "AccountCode"
        Me.GridColumn97.Name = "GridColumn97"
        Me.GridColumn97.Width = 294
        '
        'GridColumn98
        '
        Me.GridColumn98.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn98.Caption = "Nombre"
        Me.GridColumn98.FieldName = "AccountName"
        Me.GridColumn98.Name = "GridColumn98"
        Me.GridColumn98.Width = 1098
        '
        'GridColumn51
        '
        Me.GridColumn51.Caption = "Id"
        Me.GridColumn51.FieldName = "Id"
        Me.GridColumn51.Name = "GridColumn51"
        '
        'GridColumn52
        '
        Me.GridColumn52.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn52.Caption = "Código"
        Me.GridColumn52.FieldName = "AccountCode"
        Me.GridColumn52.Name = "GridColumn52"
        Me.GridColumn52.Width = 294
        '
        'GridColumn53
        '
        Me.GridColumn53.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn53.Caption = "Nombre"
        Me.GridColumn53.FieldName = "AccountName"
        Me.GridColumn53.Name = "GridColumn53"
        Me.GridColumn53.Width = 1098
        '
        'GridColumn54
        '
        Me.GridColumn54.Caption = "Id"
        Me.GridColumn54.FieldName = "Id"
        Me.GridColumn54.Name = "GridColumn54"
        '
        'GridColumn55
        '
        Me.GridColumn55.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn55.Caption = "Código"
        Me.GridColumn55.FieldName = "AccountCode"
        Me.GridColumn55.Name = "GridColumn55"
        Me.GridColumn55.Width = 294
        '
        'GridColumn56
        '
        Me.GridColumn56.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn56.Caption = "Nombre"
        Me.GridColumn56.FieldName = "AccountName"
        Me.GridColumn56.Name = "GridColumn56"
        Me.GridColumn56.Width = 1098
        '
        'GridColumn57
        '
        Me.GridColumn57.Caption = "Id"
        Me.GridColumn57.FieldName = "Id"
        Me.GridColumn57.Name = "GridColumn57"
        '
        'GridColumn58
        '
        Me.GridColumn58.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn58.Caption = "Código"
        Me.GridColumn58.FieldName = "AccountCode"
        Me.GridColumn58.Name = "GridColumn58"
        Me.GridColumn58.Width = 294
        '
        'GridColumn59
        '
        Me.GridColumn59.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn59.Caption = "Nombre"
        Me.GridColumn59.FieldName = "AccountName"
        Me.GridColumn59.Name = "GridColumn59"
        Me.GridColumn59.Width = 1098
        '
        'GridColumn60
        '
        Me.GridColumn60.Caption = "Id"
        Me.GridColumn60.FieldName = "Id"
        Me.GridColumn60.Name = "GridColumn60"
        '
        'GridColumn61
        '
        Me.GridColumn61.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn61.Caption = "Código"
        Me.GridColumn61.FieldName = "AccountCode"
        Me.GridColumn61.Name = "GridColumn61"
        Me.GridColumn61.Width = 294
        '
        'GridColumn62
        '
        Me.GridColumn62.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn62.Caption = "Nombre"
        Me.GridColumn62.FieldName = "AccountName"
        Me.GridColumn62.Name = "GridColumn62"
        Me.GridColumn62.Width = 1098
        '
        'GridColumn63
        '
        Me.GridColumn63.Caption = "Id"
        Me.GridColumn63.FieldName = "Id"
        Me.GridColumn63.Name = "GridColumn63"
        '
        'GridColumn64
        '
        Me.GridColumn64.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn64.Caption = "Código"
        Me.GridColumn64.FieldName = "AccountCode"
        Me.GridColumn64.Name = "GridColumn64"
        Me.GridColumn64.Width = 294
        '
        'GridColumn65
        '
        Me.GridColumn65.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn65.Caption = "Nombre"
        Me.GridColumn65.FieldName = "AccountName"
        Me.GridColumn65.Name = "GridColumn65"
        Me.GridColumn65.Width = 1098
        '
        'GridColumn66
        '
        Me.GridColumn66.Caption = "Id"
        Me.GridColumn66.FieldName = "Id"
        Me.GridColumn66.Name = "GridColumn66"
        '
        'GridColumn67
        '
        Me.GridColumn67.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn67.Caption = "Código"
        Me.GridColumn67.FieldName = "AccountCode"
        Me.GridColumn67.Name = "GridColumn67"
        Me.GridColumn67.Width = 294
        '
        'GridColumn68
        '
        Me.GridColumn68.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn68.Caption = "Nombre"
        Me.GridColumn68.FieldName = "AccountName"
        Me.GridColumn68.Name = "GridColumn68"
        Me.GridColumn68.Width = 1098
        '
        'GridColumn69
        '
        Me.GridColumn69.Caption = "Id"
        Me.GridColumn69.FieldName = "Id"
        Me.GridColumn69.Name = "GridColumn69"
        '
        'GridColumn70
        '
        Me.GridColumn70.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn70.Caption = "Código"
        Me.GridColumn70.FieldName = "AccountCode"
        Me.GridColumn70.Name = "GridColumn70"
        Me.GridColumn70.Width = 294
        '
        'GridColumn71
        '
        Me.GridColumn71.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn71.Caption = "Nombre"
        Me.GridColumn71.FieldName = "AccountName"
        Me.GridColumn71.Name = "GridColumn71"
        Me.GridColumn71.Width = 1098
        '
        'GridColumn72
        '
        Me.GridColumn72.Caption = "Id"
        Me.GridColumn72.FieldName = "Id"
        Me.GridColumn72.Name = "GridColumn72"
        '
        'GridColumn73
        '
        Me.GridColumn73.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn73.Caption = "Código"
        Me.GridColumn73.FieldName = "AccountCode"
        Me.GridColumn73.Name = "GridColumn73"
        Me.GridColumn73.Width = 294
        '
        'GridColumn74
        '
        Me.GridColumn74.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn74.Caption = "Nombre"
        Me.GridColumn74.FieldName = "AccountName"
        Me.GridColumn74.Name = "GridColumn74"
        Me.GridColumn74.Width = 1098
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Id"
        Me.GridColumn25.FieldName = "Id"
        Me.GridColumn25.Name = "GridColumn25"
        '
        'GridColumn26
        '
        Me.GridColumn26.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn26.Caption = "Código"
        Me.GridColumn26.FieldName = "AccountCode"
        Me.GridColumn26.Name = "GridColumn26"
        Me.GridColumn26.Width = 294
        '
        'GridColumn27
        '
        Me.GridColumn27.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn27.Caption = "Nombre"
        Me.GridColumn27.FieldName = "AccountName"
        Me.GridColumn27.Name = "GridColumn27"
        Me.GridColumn27.Width = 1098
        '
        'GridColumn28
        '
        Me.GridColumn28.Caption = "Id"
        Me.GridColumn28.FieldName = "Id"
        Me.GridColumn28.Name = "GridColumn28"
        '
        'GridColumn29
        '
        Me.GridColumn29.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn29.Caption = "Código"
        Me.GridColumn29.FieldName = "AccountCode"
        Me.GridColumn29.Name = "GridColumn29"
        Me.GridColumn29.Width = 294
        '
        'GridColumn30
        '
        Me.GridColumn30.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn30.Caption = "Nombre"
        Me.GridColumn30.FieldName = "AccountName"
        Me.GridColumn30.Name = "GridColumn30"
        Me.GridColumn30.Width = 1098
        '
        'GridColumn31
        '
        Me.GridColumn31.Caption = "Id"
        Me.GridColumn31.FieldName = "Id"
        Me.GridColumn31.Name = "GridColumn31"
        '
        'GridColumn32
        '
        Me.GridColumn32.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn32.Caption = "Código"
        Me.GridColumn32.FieldName = "AccountCode"
        Me.GridColumn32.Name = "GridColumn32"
        Me.GridColumn32.Width = 294
        '
        'GridColumn33
        '
        Me.GridColumn33.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn33.Caption = "Nombre"
        Me.GridColumn33.FieldName = "AccountName"
        Me.GridColumn33.Name = "GridColumn33"
        Me.GridColumn33.Width = 1098
        '
        'GridColumn34
        '
        Me.GridColumn34.Caption = "Id"
        Me.GridColumn34.FieldName = "Id"
        Me.GridColumn34.Name = "GridColumn34"
        '
        'GridColumn35
        '
        Me.GridColumn35.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn35.Caption = "Código"
        Me.GridColumn35.FieldName = "AccountCode"
        Me.GridColumn35.Name = "GridColumn35"
        Me.GridColumn35.Width = 294
        '
        'GridColumn36
        '
        Me.GridColumn36.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn36.Caption = "Nombre"
        Me.GridColumn36.FieldName = "AccountName"
        Me.GridColumn36.Name = "GridColumn36"
        Me.GridColumn36.Width = 1098
        '
        'GridColumn37
        '
        Me.GridColumn37.Caption = "Id"
        Me.GridColumn37.FieldName = "Id"
        Me.GridColumn37.Name = "GridColumn37"
        '
        'GridColumn38
        '
        Me.GridColumn38.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn38.Caption = "Código"
        Me.GridColumn38.FieldName = "AccountCode"
        Me.GridColumn38.Name = "GridColumn38"
        Me.GridColumn38.Width = 294
        '
        'GridColumn39
        '
        Me.GridColumn39.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn39.Caption = "Nombre"
        Me.GridColumn39.FieldName = "AccountName"
        Me.GridColumn39.Name = "GridColumn39"
        Me.GridColumn39.Width = 1098
        '
        'GridColumn40
        '
        Me.GridColumn40.Caption = "Id"
        Me.GridColumn40.FieldName = "Id"
        Me.GridColumn40.Name = "GridColumn40"
        '
        'GridColumn41
        '
        Me.GridColumn41.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn41.Caption = "Código"
        Me.GridColumn41.FieldName = "AccountCode"
        Me.GridColumn41.Name = "GridColumn41"
        Me.GridColumn41.Width = 294
        '
        'GridColumn42
        '
        Me.GridColumn42.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn42.Caption = "Nombre"
        Me.GridColumn42.FieldName = "AccountName"
        Me.GridColumn42.Name = "GridColumn42"
        Me.GridColumn42.Width = 1098
        '
        'GridColumn43
        '
        Me.GridColumn43.Caption = "Id"
        Me.GridColumn43.FieldName = "Id"
        Me.GridColumn43.Name = "GridColumn43"
        '
        'GridColumn44
        '
        Me.GridColumn44.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn44.Caption = "Código"
        Me.GridColumn44.FieldName = "AccountCode"
        Me.GridColumn44.Name = "GridColumn44"
        Me.GridColumn44.Width = 294
        '
        'GridColumn45
        '
        Me.GridColumn45.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn45.Caption = "Nombre"
        Me.GridColumn45.FieldName = "AccountName"
        Me.GridColumn45.Name = "GridColumn45"
        Me.GridColumn45.Width = 1098
        '
        'GridColumn46
        '
        Me.GridColumn46.Caption = "Id"
        Me.GridColumn46.FieldName = "Id"
        Me.GridColumn46.Name = "GridColumn46"
        '
        'GridColumn47
        '
        Me.GridColumn47.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn47.Caption = "Código"
        Me.GridColumn47.FieldName = "AccountCode"
        Me.GridColumn47.Name = "GridColumn47"
        Me.GridColumn47.Width = 294
        '
        'GridColumn48
        '
        Me.GridColumn48.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn48.Caption = "Nombre"
        Me.GridColumn48.FieldName = "AccountName"
        Me.GridColumn48.Name = "GridColumn48"
        Me.GridColumn48.Width = 1098
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Id"
        Me.GridColumn1.FieldName = "Id"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn2.Caption = "Código"
        Me.GridColumn2.FieldName = "AccountCode"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Width = 294
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn3.Caption = "Nombre"
        Me.GridColumn3.FieldName = "AccountName"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Width = 1098
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Id"
        Me.GridColumn4.FieldName = "Id"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn5.Caption = "Código"
        Me.GridColumn5.FieldName = "AccountCode"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Width = 294
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn6.Caption = "Nombre"
        Me.GridColumn6.FieldName = "AccountName"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Width = 1098
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Id"
        Me.GridColumn7.FieldName = "Id"
        Me.GridColumn7.Name = "GridColumn7"
        '
        'GridColumn8
        '
        Me.GridColumn8.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn8.Caption = "Código"
        Me.GridColumn8.FieldName = "AccountCode"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Width = 294
        '
        'GridColumn9
        '
        Me.GridColumn9.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn9.Caption = "Nombre"
        Me.GridColumn9.FieldName = "AccountName"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Width = 1098
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Id"
        Me.GridColumn10.FieldName = "Id"
        Me.GridColumn10.Name = "GridColumn10"
        '
        'GridColumn11
        '
        Me.GridColumn11.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn11.Caption = "Código"
        Me.GridColumn11.FieldName = "AccountCode"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Width = 294
        '
        'GridColumn12
        '
        Me.GridColumn12.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn12.Caption = "Nombre"
        Me.GridColumn12.FieldName = "AccountName"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Width = 1098
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Id"
        Me.GridColumn13.FieldName = "Id"
        Me.GridColumn13.Name = "GridColumn13"
        '
        'GridColumn14
        '
        Me.GridColumn14.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn14.Caption = "Código"
        Me.GridColumn14.FieldName = "AccountCode"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Width = 294
        '
        'GridColumn15
        '
        Me.GridColumn15.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn15.Caption = "Nombre"
        Me.GridColumn15.FieldName = "AccountName"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.Width = 1098
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Id"
        Me.GridColumn16.FieldName = "Id"
        Me.GridColumn16.Name = "GridColumn16"
        '
        'GridColumn17
        '
        Me.GridColumn17.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn17.Caption = "Código"
        Me.GridColumn17.FieldName = "AccountCode"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.Width = 294
        '
        'GridColumn18
        '
        Me.GridColumn18.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn18.Caption = "Nombre"
        Me.GridColumn18.FieldName = "AccountName"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.Width = 1098
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Id"
        Me.GridColumn19.FieldName = "Id"
        Me.GridColumn19.Name = "GridColumn19"
        '
        'GridColumn20
        '
        Me.GridColumn20.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn20.Caption = "Código"
        Me.GridColumn20.FieldName = "AccountCode"
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.Width = 294
        '
        'GridColumn21
        '
        Me.GridColumn21.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn21.Caption = "Nombre"
        Me.GridColumn21.FieldName = "AccountName"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.Width = 1098
        '
        'GridColumn22
        '
        Me.GridColumn22.Caption = "Id"
        Me.GridColumn22.FieldName = "Id"
        Me.GridColumn22.Name = "GridColumn22"
        '
        'GridColumn23
        '
        Me.GridColumn23.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn23.Caption = "Código"
        Me.GridColumn23.FieldName = "AccountCode"
        Me.GridColumn23.Name = "GridColumn23"
        Me.GridColumn23.Width = 294
        '
        'GridColumn24
        '
        Me.GridColumn24.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn24.Caption = "Nombre"
        Me.GridColumn24.FieldName = "AccountName"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.Width = 1098
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyAuthorization
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 614)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'GridColumn357
        '
        Me.GridColumn357.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn357.Caption = "Selección"
        Me.GridColumn357.FieldName = "Item2"
        Me.GridColumn357.Name = "GridColumn357"
        '
        'GridColumn359
        '
        Me.GridColumn359.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn359.Caption = "Selección"
        Me.GridColumn359.FieldName = "Item2"
        Me.GridColumn359.Name = "GridColumn359"
        '
        'GridColumn361
        '
        Me.GridColumn361.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn361.Caption = "Selección"
        Me.GridColumn361.FieldName = "Item2"
        Me.GridColumn361.Name = "GridColumn361"
        '
        'GridColumn3781
        '
        Me.GridColumn3781.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn3781.Caption = "Selección"
        Me.GridColumn3781.FieldName = "Item2"
        Me.GridColumn3781.Name = "GridColumn3781"
        '
        'PopUpRequests
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1467, 745)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PopUpRequests"
        Me.Opacity = 1.0R
        Me.Tag = "2178"
        Me.Text = "Autorizaciones"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPacient, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTePacient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyAuthorization, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyAuthorization.ResumeLayout(False)
        CType(Me.INDTeStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleAuthorized.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvAuthorized, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleQuoted.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvQuoted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleContracted.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvContracted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleCovered.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCovered, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleFinancedResourceUPC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvFinancedResourceUPC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeItem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeProfessional.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeHealthAdministrator.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeCareGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeFunctionalUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeCareCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeFolio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeRequestDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeRequestDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeAdmissionNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAdmissionNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCareCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFunctionalUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCareGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciHealthAdministrator, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFolio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygAuthorizationInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCovered, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciContracted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciQuoted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAuthorized, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygAditionalInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRequestDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciProfessional, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFinancedResourceUPC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlyAuthorization As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDTeAdmissionNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciAdmissionNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTePacient As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciPacient As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygGeneralInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn33 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn34 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn35 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn36 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn37 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn38 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn39 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn40 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn41 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn42 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn43 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn44 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn45 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn46 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn47 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn48 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn51 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn52 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn53 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn54 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn55 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn56 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn57 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn58 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn59 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn60 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn61 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn62 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn63 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn64 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn65 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn66 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn67 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn68 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn69 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn70 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn71 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn72 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn73 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn74 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn75 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn76 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn77 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn78 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn79 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn80 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn81 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn82 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn83 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn84 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn85 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn86 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn87 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn88 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn89 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn90 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn91 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn92 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn93 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn94 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn95 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn96 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn97 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn98 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn99 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn100 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn101 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn102 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn103 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn104 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn105 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn106 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn107 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn108 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn109 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn110 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn111 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn112 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn113 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn114 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn115 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn116 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn117 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn118 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn119 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn120 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn121 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn122 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn123 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn124 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn125 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn126 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn127 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn128 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn129 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn130 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn131 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn132 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn133 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn134 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn135 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn136 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn137 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn138 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn139 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn140 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn141 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn142 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn143 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn144 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn145 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn146 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn147 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn148 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn149 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn150 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn151 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn152 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn153 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn154 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn155 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn156 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn157 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn158 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn159 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn160 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn161 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn162 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn163 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn164 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn165 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn166 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn167 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn168 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn169 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn170 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn171 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn172 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn173 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn174 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn175 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn176 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn177 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn178 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn179 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn180 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn181 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn182 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn183 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn184 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn185 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn186 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn187 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn188 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn189 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn190 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn191 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn192 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn193 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn194 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn195 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn196 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn197 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn198 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn199 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn200 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn201 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn202 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn203 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn204 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn205 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn206 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn207 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn208 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn209 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn210 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn211 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn212 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn213 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn214 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn215 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn216 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn217 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn218 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn219 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn220 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn221 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn222 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn223 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn224 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn225 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn226 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn227 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn228 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn229 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn230 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn231 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn232 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn233 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn234 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn235 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn236 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn237 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn238 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn239 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn240 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn241 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn242 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn243 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn244 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn245 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn246 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn247 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn248 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn249 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn250 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn251 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn252 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn253 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn254 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn255 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn256 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn257 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn258 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn259 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn260 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn261 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn262 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn263 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn264 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn265 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn266 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn267 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn268 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn269 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn270 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn271 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn272 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn273 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn274 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn275 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn276 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn277 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn278 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn279 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn280 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn281 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn282 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn283 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn284 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn285 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn286 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn287 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn288 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn289 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn290 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn291 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn292 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn293 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn294 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn295 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn296 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn297 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn298 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn299 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn300 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn301 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn302 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn303 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn304 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn305 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn306 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn307 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn308 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn309 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn310 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn311 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn312 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn313 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn314 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn315 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn316 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn317 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn318 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn319 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn320 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn321 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn322 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn323 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn324 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn325 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn326 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn327 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn328 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn329 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn330 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn331 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn332 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn333 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn334 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn335 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn336 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn337 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn338 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn339 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn340 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn341 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn342 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn343 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn344 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn345 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn346 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn347 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn348 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn349 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn350 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn351 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn352 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn353 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn354 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn358 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlygAuthorizationInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridColumn357 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn360 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn359 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn362 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn361 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn363 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn364 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn365 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn366 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn367 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn368 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn369 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn370 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn371 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn372 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn373 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn374 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn375 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn376 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn377 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDDeRequestDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLciRequestDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygAditionalInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTeHealthAdministrator As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTeCareGroup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTeFunctionalUnit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTeCareCenter As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTeFolio As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciCareCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciFunctionalUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciCareGroup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciHealthAdministrator As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciFolio As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTeQuantity As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTeItem As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTeProfessional As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciProfessional As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciItem As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3781 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGleAuthorized As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents INDGvAuthorized As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGleQuoted As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents INDGvQuoted As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGleContracted As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents INDGvContracted As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGleCovered As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents INDGvCovered As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGleFinancedResourceUPC As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents INDGvFinancedResourceUPC As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGleType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents INDGvType As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciCovered As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciContracted As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciQuoted As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciAuthorized As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciFinancedResourceUPC As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvType_Description As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvFinancedResourceUPC_Description As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvCovered_Description As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvContracted_Description As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvQuoted_Description As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvAuthorized_Description As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDTeStatus As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciStatus As DevExpress.XtraLayout.LayoutControlItem
End Class
