# Tweet a Random Feed Item (Ghost, AWS Lambda, and Tweetinvi)

My first experience with AWS Lambda was [using Ephemeral to clean up my Twitter feed](https://grantwinney.com/my-first-experience-with-aws-lambda/), which I lightly modified to delete "likes" as well as tweets.

Ephemeral is a Go app written by [Vicky Lai](https://vickylai.com/) - check out her posts too:

* [Running a free Twitter bot on AWS Lambda](https://vickylai.com/verbose/free-twitter-bot-aws-lambda/#setting-up-aws-lambda "Running a free Twitter bot on AWS Lambda") _(intro on how to setup AWS Lambda)_
* [Why I’m automatically deleting all my old tweets, and the AWS Lambda function I use to do this](https://medium.freecodecamp.org/why-im-automatically-deleting-all-my-old-tweets-and-the-aws-lambda-function-i-use-to-do-this-6d26ef517ee1 "Why I’m automatically deleting all my old tweets, and the AWS Lambda function I use to do this") _(food for thought, and how to setup Ephemeral)_

## My Turn

I wanted to try my hand at writing my own app to run in AWS Lambda, and I wanted to do it in C#. I've also had an idea for awhile now that it'd be nice to be able to randomly select and tweet my old blog posts.

