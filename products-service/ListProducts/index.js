module.exports = function (context, req) {
    context.log('Start ListProducts...');

    // TODO: think about paging!
    context.res.status(200).json(context.bindings.products
        .map(p => {
            return { id: p.RowKey, name: p.Name }
        }));
};