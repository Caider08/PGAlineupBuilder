$(function (){
  $("#tabs").tabs();
});
  
//var ManageArticles = {};



/*$("#tabs").tabs({
  show: function (event, ui){
    if (!ui.tab.isLoaded) {

      var gridMgr = ManageArticles.GridManager, fn, gridName, pagerName;

            switch (ui.index) {
                case 0:
                    // call function to create grid for managing posts
                    // from "#tablePosts" and "#pagerPosts"
                    fn = gridMgr.postsGrid;
                    gridName = ""#tablePosts";
                    pagerName = "#pagerPosts";
                    break;
                case 1:
                    // call function to create grid for managing posts
                    // from "#tableCats" and "#pagerCats"
                    fn = gridMgr.categoriesGrid;
                    gridName = "#tableCats";
                    pagerName = "#pagerCats";
                    break;
                case 2:
                    // call function to create grid for managing posts
                    // from "#tableTags" and "#pagerTags";
                    fn = gridMgr.tagsGrid;
                    gridName = "#tableTags";
                    pagerName = "#pagerTags";
                    break;
            };
            fn(gridName, pagerName);
            ui.tab.isLoaded = true;
          }

  }
})


ManageArticles.GridManager = {

    //function to create grid to manage posts
    postsGrid: function(gridName, pagerName){
      // columns
    var colNames = [
        'Id',
        'Name',
        'Content',
        'CategoryID',
        'Category',
        'TagID',
        'Tag',
        'Meta',
        'Url Slug',
        'PublishedDate',
        'Modified'
    ];

    var columns = [];

    columns.push({
        name: 'Id',
        hidden: true,
        key: true
    });

    columns.push({
        name: 'Name',
        index: 'Name',
        width: 250
    });

    columns.push({
        name: 'Content',
        width: 250,
        sortable: false,
        hidden: true
    });

    columns.push({
        name: 'CategoryID',
        hidden: true
    });

    columns.push({
        name: 'Category.Name',
        index: 'Category',
        width: 150
    });

    columns.push({
        name: 'TagID',
        hidden: true
    });

    columns.push({
        name: 'Tag.Name',
        index: 'Tag'
        width: 150
    });

    columns.push({
        name: 'Meta',
        width: 250,
        sortable: false
    });

    columns.push({
        name: 'UrlSlug',
        width: 200,
        sortable: false
    });

    columns.push({
        name: 'PublishedDate',
        index: 'PublishedDate',
        width: 150,
        align: 'center'
        sorttype: 'date',
        datefmt: 'm/d/Y',
    });


    columns.push({
        name: 'Modified',
        index: 'Modified',
        width: 150,
        align: 'center',
        sorttype: 'date',
        datefmt: 'm/d/Y'
    });

    // create the grid
    $(gridName).jqGrid({
        // server url and other ajax stuff
        url: '/ManageGolfArticles/Posts',
        datatype: 'json',
        mtype: 'GET',

        height: 'auto',

        // columns
        colNames: colNames,
        colModel: columns,

        // pagination options
        toppager: true,
        pager: pagerName,
        rowNum: 10,
        rowList: [10, 20, 30],

        // row number column
        rownumbers: true,
        rownumWidth: 40,

        // default sorting
        sortname: 'PublishedDate',
        sortorder: 'desc',

        // display the no. of records message
        viewrecords: true,

        jsonReader: { repeatitems: false }
    });

    }

    categoriesGrid: function(gridName, pagerName){},

    tagsGrid: function(gridName, pagerName){}
};

});
*/
