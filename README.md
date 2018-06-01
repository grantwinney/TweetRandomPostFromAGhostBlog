# Tweet a Random Feed Item

***Using [Ghost](https://ghost.org/), [AWS Lambda](https://aws.amazon.com/lambda/), and [Tweetinvi](https://github.com/linvi/tweetinvi)***

My first experience with AWS Lambda was [using Ephemeral to clean up my Twitter feed](https://grantwinney.com/my-first-experience-with-aws-lambda/), which I lightly modified to delete "likes" as well as tweets.

Ephemeral is a Go app written by [Vicky Lai](https://vickylai.com/) - check out her posts too:

* [Running a free Twitter bot on AWS Lambda](https://vickylai.com/verbose/free-twitter-bot-aws-lambda/#setting-up-aws-lambda "Running a free Twitter bot on AWS Lambda") _(intro on how to setup AWS Lambda)_
* [Why I'm automatically deleting all my old tweets, and the AWS Lambda function I use to do this](https://medium.freecodecamp.org/why-im-automatically-deleting-all-my-old-tweets-and-the-aws-lambda-function-i-use-to-do-this-6d26ef517ee1 "Why I'm automatically deleting all my old tweets, and the AWS Lambda function I use to do this") _(food for thought, and how to setup Ephemeral)_

I wanted to try my hand at writing my own app to run in AWS Lambda, and I wanted to do it in C#. I've also had an idea for awhile now that it'd be nice to be able to randomly select and tweet my old blog posts.

## Usage

So here it is, a C# console app you can run in AWS Lambda. Schedule it to run as often as you like - I'm thinking maybe once a day.

### Getting the code

1. Clone the repo: `git clone git@github.com:grantwinney/TweetRandomFeedItem.git`
2. Open the project in Visual Studio.
3. Build the project _(you may have to right-click the NuGet folder and choose 'restore' to download the dependencies, although VS usually takes care of that for you)_
4. Find the `bin` directory on disk, and drill down until you get to the assemblies (dll files). Most likely: `bin/Debug/netcoreapp2.0`
5. Zip up the contents of the inner-most directory, but not the directory itself. _(e.g. just select all the files_ inside _`netcoreapp2.0`.)_

### Setting up AWS Lambda

Now you need to create a new AWS Lambda job. Check out the first article linked above for a brief intro to setting up a job.

1. Create a new function and choose `C# (.NET Core 2.0)` for the runtime.
2. The name of the function, and the role it makes you create, don't matter.
3. Upload the zip file you created above.
4. Set the handler as `TweetRandomFeedItem::TweetRandomFeedItem.Program::Main`
5. Under "Basic settings", decrease the memory to 128MB and increase the timeout to a minute. For me, it generally takes about 15-20 seconds to run, and uses 50MB or less of memory.
6. Set the environment variables as follows:

#### Environment Variables

I make heavy use of environment variables, so credentials and other settings can easily be changed between runs, without having to recompile the code and upload it again.

| Field        | Required           | Description  |
| ------------- |:-------------:| -----|
| `GHOST_CLIENT_ID`<br>`GHOST_CLIENT_SECRET` | Yes | If you don't have the public API enabled in Ghost, do it now. Open any post on your blog and view the source code for something like the folowing - you'll need both values.<br><br>`ghost.init({clientId: "ghost-frontend", clientSecret: "55f1e0f55123"});`
| `GHOST_USERNAME`<br>`GHOST_PASSWORD` | No | If you leave the public API enabled, you can omit these variables (or include them but leave the value blank).<br><br>If you have the public API _disabled,_ you need to include your Ghost username and password too, so that the app can retrieve an auth token from Ghost. |
| `GHOST_URI`   | Yes | Specify your blog URL, like: `https://grantwinney.com`<br><br>Without this, the app can't make an API call to your blog to select a random post. |
| `GHOST_POST_RETRIEVAL_LIMIT`      | No | Specify the number of posts you want to retrieve. It uses the [limit](https://api.ghost.org/docs/limit) parameter of the API.<br><br>If you omit this, or leave the value empty, the app will consider *all* your posts when selecting a random one. |
| `TWITTER_CONSUMER_KEY`<br>`TWITTER_CONSUMER_SECRET`<br>`TWITTER_USER_ACCESS_TOKEN`<br>`TWITTER_USER_ACCESS_TOKEN_SECRET`      | Yes      | These values all come from your Twitter account. You need to create a new app to get these values, which allows you to post tweets. |
| `TWITTER_MAX_TAG_COUNT` | No      | Although tag spamming is somewhat prevalent on Facebook, and even more-so on Instagram, I don't see a lot of it on Twitter. If you tend to use a lot of tags for posts on your blog, then you can limit how many of those transfer to your tweet.<br><br>If you omit this, or leave the value empty, the app will use the first 3 tags. |
