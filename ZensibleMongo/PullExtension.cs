﻿namespace ZensibleMongo
{
    using System.Threading;
    using System.Threading.Tasks;
    using Filter;
    using Interfaces;
    using MongoDB.Driver;

    /// <summary>
    /// Extension for handling pull from mongo collection
    /// </summary>
    public static class PullExtension
    {
        /// <summary>
        /// Pulls the multiple documents from the mongo db based on the filter recipe
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="recipe"></param>
        /// <param name="token">cancellation token</param>
        /// <returns></returns>
        public static async Task<IAsyncCursor<TDocument>> Pull<TDocument>(
            this IFilterDefinition<TDocument> recipe,
            CancellationToken token = default(CancellationToken))
        {
            var filter = Factory.Combine(recipe.Filters());

            return await recipe.Collection.FindAsync(filter, null, token);
        }

        /// <summary>
        /// Pulls the a single document from the mongo db based on the filter recipe
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="recipe"></param>
        /// <param name="token">cancellation token</param>
        /// <returns></returns>
        public static async Task<TDocument> Pull<TDocument>(
            this ForSingleWhere<TDocument> recipe,
            CancellationToken token = default(CancellationToken))
        {
            var filter = Factory.Combine(recipe.Filters());
            return await recipe.Collection.FindAsync(filter, null, token).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Pulls the a single document from the mongo db based on the filter recipe
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="recipe"></param>
        /// <param name="token">cancellation token</param>
        /// <returns></returns>
        public static async Task<TDocument> Pull<TDocument>(
            this ForIdRecipe<TDocument> recipe,
            CancellationToken token = default(CancellationToken))
        {
            var filter = Factory.Combine(recipe.Filters());
            return await recipe.Collection.FindAsync(filter, null, token).FirstOrDefaultAsync();
        }
    }
}