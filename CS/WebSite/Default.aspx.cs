using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web;
using DevExpress.Web.Data;

public partial class _Default : System.Web.UI.Page {
    protected void Grid_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e) {
        if(e.Column.FieldName == "TagIDs") {
            var tagIDs = (int[])e.Value;

            var text = DataProvider.GetTags().Where(t => tagIDs.Contains(t.ID)).
                Select(t => t.Name).DefaultIfEmpty().Aggregate((a, b) => a + ", " + b);

            e.DisplayText = text ?? string.Empty;
        }
    }

    protected void Grid_RowInserting(object sender, ASPxDataInsertingEventArgs e) {
        e.NewValues["TagIDs"] = GetTags();
    }

    protected void Grid_RowUpdating(object sender, ASPxDataUpdatingEventArgs e) {
        e.NewValues["TagIDs"] = GetTags();
    }

    protected void Lookup_Init(object sender, EventArgs e) {
        var lookup = (ASPxGridLookup)sender;
        var container = (GridViewEditItemTemplateContainer)lookup.NamingContainer;

        if (container.Grid.IsNewRowEditing)
            return;

        var tagIDs = (int[])container.Grid.GetRowValues(container.VisibleIndex, container.Column.FieldName);
        if(tagIDs != null) {
            foreach(var tagID in tagIDs)
                lookup.GridView.Selection.SelectRowByKey(tagID);
        }
    }

    private int[] GetTags() {
        var column = (GridViewDataColumn)Grid.Columns["TagIDs"];
        var lookup = (ASPxGridLookup)Grid.FindEditRowCellTemplateControl(column, "Lookup");
        var tags = lookup.GridView.GetSelectedFieldValues(lookup.KeyFieldName) as List<object>;

        return tags.Select(t => (int)t).ToArray();
    }
}