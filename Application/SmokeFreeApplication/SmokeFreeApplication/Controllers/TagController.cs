using System;
using System.Collections.Generic;
using System.Linq;
using SmokeFreeApplication.Models;


namespace SmokeFreeApplication.Controllers
{
    public class TagController
    {
        private SmokeFreeDBContext smokeFreeDB = new SmokeFreeDBContext();
        public List<String> getTags(int? id)
        {
            List<StoriesTag> storyTags = smokeFreeDB.StoriesTag.Where(x => x.storyID == id).ToList();
            List<String> displayList = new List<string>();
            Tag tagItem;
            int temp;
            for (int i = 0; i < storyTags.Count; i++)
            {
                temp = storyTags[i].tagID;
                tagItem = smokeFreeDB.Tag.Where(x => x.tagID == temp).FirstOrDefault();
                displayList.Add(tagItem.tagName);
            }
            return displayList;
        }
        public List<String> getArticleTags(int? id)
        {
            List<ArticlesTag> articleTags = smokeFreeDB.ArticlesTag.Where(x => x.articleID == id).ToList();
            List<String> displayList = new List<string>();
            Tag tagItem;
            int temp;
            for (int i = 0; i < articleTags.Count; i++)
            {
                temp = articleTags[i].tagID;
                tagItem = smokeFreeDB.Tag.Where(x => x.tagID == temp).FirstOrDefault();
                displayList.Add(tagItem.tagName);
            }
            return displayList;
        }
        public void saveTags(string inputTags, int storyID)
        {
            string[] tagArray = inputTags.Split(',');
            Tag[] tagList = smokeFreeDB.Tag.ToArray();
            for (int i = 0; i < tagArray.Length; i++)
            {
                string tmp = tagArray[i];
                Tag tag = smokeFreeDB.Tag.Where(x => x.tagName == tmp).FirstOrDefault();
                StoriesTag storyTag = new StoriesTag();
                if (tag != null)
                {
                    // Tag is found
                    storyTag.tagID = tag.tagID;
                    storyTag.storyID = storyID;
                    smokeFreeDB.StoriesTag.Add(storyTag);
                    smokeFreeDB.SaveChanges();
                }
                else
                {
                    //Tag is not found in database
                    Tag newTag = new Tag();
                    newTag.tagName = tagArray[i];
                    smokeFreeDB.Tag.Add(newTag);
                    smokeFreeDB.SaveChanges();
                    smokeFreeDB.Entry(newTag).Reload();
                    storyTag.tagID = newTag.tagID;
                    storyTag.storyID = storyID;
                    smokeFreeDB.StoriesTag.Add(storyTag);
                    smokeFreeDB.SaveChanges();

                }
            }


        }
        public void saveArticleTags(string inputTags, int articleId)
        {
            string[] tagArray = inputTags.Split(',');
            Tag[] tagList = smokeFreeDB.Tag.ToArray();
            for (int i = 0; i < tagArray.Length; i++)
            {
                string tmp = tagArray[i];
                Tag tag = smokeFreeDB.Tag.Where(x => x.tagName == tmp).FirstOrDefault();
                ArticlesTag articleTag = new ArticlesTag();
                if (tag != null)
                {
                    // Tag is found
                    articleTag.tagID = tag.tagID;
                    articleTag.articleID = articleId;
                    smokeFreeDB.ArticlesTag.Add(articleTag);
                    smokeFreeDB.SaveChanges();
                }
                else
                {
                    //Tag is not found in database
                    Tag newTag = new Tag();
                    newTag.tagName = tagArray[i];
                    smokeFreeDB.Tag.Add(newTag);
                    smokeFreeDB.SaveChanges();
                    smokeFreeDB.Entry(newTag).Reload();
                    articleTag.tagID = newTag.tagID;
                    articleTag.articleID = articleId;
                    smokeFreeDB.ArticlesTag.Add(articleTag);
                    smokeFreeDB.SaveChanges();

                }
            }


        }
        public List<Story> searchStoryByTag(string inputTags)
        {
            string[] tagArray = inputTags.Split(',');
            string tempTagName;
            int tempId;
            int[] tagIdArray = new int[tagArray.Length];
            List<Story> displayList = new List<Story>();
            for (int i = 0; i < tagArray.Length; i++)
            {
                tempTagName = tagArray[i];
                Tag tag = smokeFreeDB.Tag.Where(x => x.tagName == tempTagName).FirstOrDefault();
                if (tag != null)
                {
                    tagIdArray[i] = tag.tagID;
                }
            }
            for (int i = 0; i < tagIdArray.Length; i++)
            {
                tempId = tagIdArray[i];
                List<StoriesTag> storyTags = smokeFreeDB.StoriesTag.Where(x => x.tagID == tempId).ToList();
                for (int j = 0; j < storyTags.Count; j++)
                {
                    tempId = storyTags[j].storyID;
                    displayList.Add(smokeFreeDB.Story.Where(x => x.storyID == tempId).FirstOrDefault());
                }

            }
            return displayList;
        }
        public List<Article> searchArticleByTag(string inputTags)
        {
            string[] tagArray = inputTags.Split(',');
            string tempTagName;
            int tempId;
            int[] tagIdArray = new int[tagArray.Length];
            List<Article> displayList = new List<Article>();
            for (int i = 0; i < tagArray.Length; i++)
            {
                tempTagName = tagArray[i];
                Tag tag = smokeFreeDB.Tag.Where(x => x.tagName == tempTagName).FirstOrDefault();
                if (tag != null)
                {
                    tagIdArray[i] = tag.tagID;
                }
            }
            for (int i = 0; i < tagIdArray.Length; i++)
            {
                tempId = tagIdArray[i];
                List<ArticlesTag> articleTags = smokeFreeDB.ArticlesTag.Where(x => x.tagID == tempId).ToList();
                for (int j = 0; j < articleTags.Count; j++)
                {
                    tempId = articleTags[j].articleID;
                    displayList.Add(smokeFreeDB.Article.Where(x => x.articleID == tempId && x.articleStatus=="approved").FirstOrDefault());
                }

            }
            return displayList;
        }


    }

}